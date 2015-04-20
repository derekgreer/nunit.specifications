using System;
using System.Diagnostics;
#if NET20
using Actionx = NUnit.Specifications.Action2;
#else
using Actionx = System.Action;
#endif

namespace NUnit.Specifications
{
  [DebuggerStepThrough]
  public static class Catch
  {
    public static Exception Exception(Actionx action)
    {
      Exception exception = null;

      try
      {
        action();
      }
      catch (Exception e)
      {
        exception = e;
      }

      return exception;
    }
  }
}