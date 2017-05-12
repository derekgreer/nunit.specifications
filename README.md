# NUnit.Specifications

NUnit.Specifications is a context/specification library for use with the NUnit testing framework.  The API is patterned after the Machine.Specifications context/specification library.

# Quickstart

The following is an example NUnit test written using the ContextSpecification based class from NUnit.Specifications using the Should assertion library:

```C#
using NUnit.Specifications;
using Should;

public class OrderSpecs
{
	[Subject("Order Processing")]
	public class when_a_customer_places_an_order: ContextSpecification
	{
		static OrderService _orderService;
		static bool _results;
		static Order _order;
		
		Establish context = () =>
		{
			_orderService = new OrderService();
			_order = new Order();
		};
		
		Because of = () => _results = _orderService.PlaceOrder(_order);

		It should_successfully_place_the_order = () => _results.ShouldBeTrue();
	}
}
```

# Platform

NUnit.Specifications 2.0.0 targets .Net Standard Library version 1.6 and has been tested with Visual Studio 2017.

# Test Runners
NUnit.Specifications is supported by the [Visual Studio Test Platform](https://github.com/Microsoft/vstest) (VSTest) and the dotnet test CLI runner.

To create test projects within Visual Studio 2017, create a test project targeting .netcoreapp1.1 and reference the following packages:

```xml
  <ItemGroup>
    <PackageReference Include="NUnit" Version="3.6.1" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="15.0.0" />
    <PackageReference Include="NUnit3TestAdapter" Version="4.0.0-ci-00452-pr-313" />
  </ItemGroup>
```

Currently, a pre-release version of the NUnit3TestAdaptor is required for .Net Core support.  To retrieve the NUnit3TestAdaptor, add the following NuGet.Config file to your test project:

```xml
<?xml version="1.0" encoding="utf-8"?>

<configuration>
  <packageSources>
    <add key="nuget.org" value="https://nuget.org/api/v2/" />
    <add key="AppVeyor NUnit CI Feed" value="https://ci.appveyor.com/nuget/nunit" />
    <add key="AppVeyor NUnit Engine CI Feed" value="https://ci.appveyor.com/nuget/nunit-console" />
    <add key="AppVeyor NUnit Adapter CI Feed" value="https://ci.appveyor.com/nuget/nunit3-vs-adapter" />
    <add key="NUnit MyGet Feed" value="https://www.myget.org/F/nunit/api/v2" />
  </packageSources>

</configuration>
```

# Non-Supporting Test Runners
In general, any test runner that supports .Net Core and the full NUnit 3.x API should support NUnit.Specifications.

As of the 2.0.0 release, tests written with NUnit.Specifications do not appear to be supported by the following test runners:

 - Resharper 2017.1
 - TestDriven.Net
 - NUnit GUI


