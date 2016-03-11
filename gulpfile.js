/// <binding ProjectOpened='build-common-assembly-info' />
//-----------------------------------------------------------------------------
// NUnit.Specifications Build
//-----------------------------------------------------------------------------
var gulp = require("gulp"),
  fs = require("fs"),
  del = require("del"),
  glob = require("glob"),
  path = require("path"),
  stream = require("stream"),
  source = require("vinyl-source-stream"),
  spawn = require("child_process").spawn,
  config = require("./package.json"),
  which = require("which").sync,
  $ = require("gulp-load-plugins")({ lazy: true });


var nunitConsoleVersion = "3.2.0";

var paths = function() {
  var p = {};
  var root = path.resolve(".");

  p.buildPath = root + "/build";
  p.distPath = root + "/dist";
  p.sourcePath = root + "/src";
  p.buildPackagesPath = p.buildPath + "/packages";
  p.nuspec = root + "/src/nuspec";

  p.solution = p.sourcePath + "/" + config.name + ".sln";
  p.nuget = p.sourcePath + "/.nuget/NuGet.exe";
  p.testRunner = p.buildPackagesPath + "/NUnit.Console." + nunitConsoleVersion + "/bin/nunit3-console.exe";
  return p;
}();

gulp.task("default", $.sequence("clean", "build-common-assembly-info", "build", "specs", "package"));

gulp.task("clean", function(cb) {
  console.log("[Cleaning Build Paths]");
  del([paths.distPath, paths.buildPath], cb());
});

gulp.task("build", ["build-compile"], function(cb) {
  ["net20", "net40", "net45"].forEach(function(version) {
    var project = paths.sourcePath + "/" + config.name + "." + version;
    gulp.src(project + "/bin/Release/**/")
      .pipe(gulp.dest(paths.buildPath + "/NUnit2/" + version));
  });

  ["net20", "net40", "net45"].forEach(function (version) {
    var project = paths.sourcePath + "/" + config.name + ".NUnit3." + version;
    gulp.src(project + "/bin/Release/**/")
      .pipe(gulp.dest(paths.buildPath + "/NUnit3/" + version));
  });
  cb();
});

gulp.task("build-compile", function(cb) {
  console.log("[Building project]");
  console.log(paths.solution);
  return gulp.src(paths.solution)
    .pipe($.msbuild({
      toolsVersion: 14.0,
      configuration: "Release",
      targets: ["Clean", "Build"],
      errorOnFail: true,
      stdout: true
    }));
});

gulp.task("specs", ["download-nunit-console", "build"], function(cb) {
  console.log("[Running Specifications]");

  var cmd = paths.testRunner;

  glob(paths.sourcePath + "/**/bin/Release/*.Specs.dll", function(globError, files) {
    if (globError) return cb(globError);

    return spawn(cmd, ["-labels=All"].concat(files), { stdio: "inherit" }).on("exit", cb);
  });
});

gulp.task("download-nunit-console", function(cb) {
  spawn(paths.nuget, ["install", "NUnit.Console", "-Version", nunitConsoleVersion, "-OutputDirectory", paths.buildPackagesPath], { stdio: "inherit" }).on("exit", cb);
});


gulp.task("package", function(cb) {
  console.log("[Creating NuGet Package]");
  fs.mkdirSync(paths.distPath);
  glob(paths.nuspec + "/**/*.nuspec", function(globError, files) {
    console.log(files);
    files.forEach(function(specFile) {
      spawn(paths.nuget, ["pack", specFile, "-OutputDirectory", paths.distPath, "-Version", config.version, "-Symbols", "-BasePath", "."], { stdio: "inherit" });
    });
  });
  cb();
});

gulp.task("publish", function(cb) {
  console.log("[Publishing NuGet Package]");
  glob(paths.distPath + "/**/*.nupkg", function (globError, files) {
    console.log(files);
    files.forEach(function (file) {
      spawn(paths.nuget, ["push", file], { stdio: "inherit" });
    });
  });
  cb();
});

gulp.task("build-common-assembly-info", function(cb) {
  console.log("[Create CommonAssemblyInfo.cs]");
  var readable = new stream.Readable;
  readable.push("using System.Reflection;\n");
  readable.push("[assembly: AssemblyProduct(\"@product\")]\n".replace(/@product/, config.name));
  readable.push("[assembly: AssemblyVersion(\"@version\")]\n".replace(/@version/, config.version));
  readable.push("[assembly: AssemblyFileVersion(\"@version\")]\n".replace(/@version/, config.version));
  readable.push(null);
  readable
    .pipe(source("CommonAssemblyInfo.cs"))
    .pipe(gulp.dest(paths.sourcePath));
  cb();
});
