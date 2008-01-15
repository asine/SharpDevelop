// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="David Srbecký" email="dsrbecky@gmail.com"/>
//     <version>$Revision$</version>
// </file>

using System;

namespace Debugger.Tests.TestPrograms
{
	public class FunctionVariablesLifetime
	{
		public int @class = 3;
		
		public static void Main()
		{
			new FunctionVariablesLifetime().Function(1);
			System.Diagnostics.Debugger.Break(); // 5
		}
		
		void Function(int argument)
		{
			int local = 2;
			System.Diagnostics.Debugger.Break(); // 1
			SubFunction();
			System.Diagnostics.Debugger.Break(); // 3
			SubFunction();
		}
		
		void SubFunction()
		{
			int localInSubFunction = 4;
			System.Diagnostics.Debugger.Break(); // 2, 4
		}
	}
}

#if TEST_CODE
namespace Debugger.Tests {
	public partial class DebuggerTests
	{
		[NUnit.Framework.Test]
		public void FunctionVariablesLifetime()
		{
			Value argument = null;
			Value local    = null;
			Value localInSubFunction = null;
			Value @class   = null;
			
			StartTest("FunctionVariablesLifetime"); // 1 - Enter program
			WaitForPause();
			argument = process.SelectedStackFrame.GetArgumentValue(0);
			local = process.SelectedStackFrame.LocalVariables["local"];
			@class = process.SelectedStackFrame.ContaingClassVariables["class"];
			ObjectDump("argument", argument);
			ObjectDump("local", local);
			ObjectDump("@class", @class);
			
			process.Continue(); // 2 - Go to the SubFunction
			WaitForPause();
			localInSubFunction = process.SelectedStackFrame.LocalVariables["localInSubFunction"];
			ObjectDump("argument", argument);
			ObjectDump("local", local);
			ObjectDump("@class", @class);
			ObjectDump("localInSubFunction", @localInSubFunction);
			
			process.Continue(); // 3 - Go back to Function
			WaitForPause();
			ObjectDump("argument", argument);
			ObjectDump("local", local);
			ObjectDump("@class", @class);
			ObjectDump("localInSubFunction", @localInSubFunction);
			
			process.Continue(); // 4 - Go to the SubFunction
			WaitForPause();
			ObjectDump("argument", argument);
			ObjectDump("local", local);
			ObjectDump("@class", @class);
			ObjectDump("localInSubFunction", @localInSubFunction);
			localInSubFunction = process.SelectedStackFrame.LocalVariables["localInSubFunction"];
			ObjectDump("localInSubFunction(new)", @localInSubFunction);
			
			process.Continue(); // 5 - Setp out of both functions
			WaitForPause();
			ObjectDump("argument", argument);
			ObjectDump("local", local);
			ObjectDump("@class", @class);
			ObjectDump("localInSubFunction", @localInSubFunction);
			
			process.Continue();
			process.WaitForExit();
			CheckXmlOutput();
		}
	}
}
#endif

#if EXPECTED_OUTPUT
<?xml version="1.0" encoding="utf-8"?>
<DebuggerTests>
  <Test name="FunctionVariablesLifetime">
    <ProcessStarted />
    <ModuleLoaded symbols="False">mscorlib.dll</ModuleLoaded>
    <ModuleLoaded symbols="True">FunctionVariablesLifetime.exe</ModuleLoaded>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="argument" Type="Value">
      <IsArray>False</IsArray>
      <ArrayLenght exception="Value is not an array" />
      <ArrayRank exception="Value is not an array" />
      <ArrayDimensions exception="Value is not an array" />
      <IsObject>False</IsObject>
      <IsPrimitive>True</IsPrimitive>
      <IsInteger>True</IsInteger>
      <PrimitiveValue>1</PrimitiveValue>
      <Expression>argument</Expression>
      <Name>argument</Name>
      <IsNull>False</IsNull>
      <AsString>1</AsString>
      <HasExpired>False</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="local" Type="Value">
      <IsArray>False</IsArray>
      <ArrayLenght exception="Value is not an array" />
      <ArrayRank exception="Value is not an array" />
      <ArrayDimensions exception="Value is not an array" />
      <IsObject>False</IsObject>
      <IsPrimitive>True</IsPrimitive>
      <IsInteger>True</IsInteger>
      <PrimitiveValue>2</PrimitiveValue>
      <Expression>local</Expression>
      <Name>local</Name>
      <IsNull>False</IsNull>
      <AsString>2</AsString>
      <HasExpired>False</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="@class" Type="Value">
      <IsArray>False</IsArray>
      <ArrayLenght exception="Value is not an array" />
      <ArrayRank exception="Value is not an array" />
      <ArrayDimensions exception="Value is not an array" />
      <IsObject>False</IsObject>
      <IsPrimitive>True</IsPrimitive>
      <IsInteger>True</IsInteger>
      <PrimitiveValue>3</PrimitiveValue>
      <Expression>this.class</Expression>
      <Name>class</Name>
      <IsNull>False</IsNull>
      <AsString>3</AsString>
      <HasExpired>False</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="argument" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>argument</Expression>
      <Name>argument</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="local" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>local</Expression>
      <Name>local</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="@class" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>this.class</Expression>
      <Name>class</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="localInSubFunction" Type="Value">
      <IsArray>False</IsArray>
      <ArrayLenght exception="Value is not an array" />
      <ArrayRank exception="Value is not an array" />
      <ArrayDimensions exception="Value is not an array" />
      <IsObject>False</IsObject>
      <IsPrimitive>True</IsPrimitive>
      <IsInteger>True</IsInteger>
      <PrimitiveValue>4</PrimitiveValue>
      <Expression>localInSubFunction</Expression>
      <Name>localInSubFunction</Name>
      <IsNull>False</IsNull>
      <AsString>4</AsString>
      <HasExpired>False</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="argument" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>argument</Expression>
      <Name>argument</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="local" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>local</Expression>
      <Name>local</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="@class" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>this.class</Expression>
      <Name>class</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="localInSubFunction" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>localInSubFunction</Expression>
      <Name>localInSubFunction</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="argument" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>argument</Expression>
      <Name>argument</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="local" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>local</Expression>
      <Name>local</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="@class" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>this.class</Expression>
      <Name>class</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="localInSubFunction" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>localInSubFunction</Expression>
      <Name>localInSubFunction</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="localInSubFunction(new)" Type="Value">
      <IsArray>False</IsArray>
      <ArrayLenght exception="Value is not an array" />
      <ArrayRank exception="Value is not an array" />
      <ArrayDimensions exception="Value is not an array" />
      <IsObject>False</IsObject>
      <IsPrimitive>True</IsPrimitive>
      <IsInteger>True</IsInteger>
      <PrimitiveValue>4</PrimitiveValue>
      <Expression>localInSubFunction</Expression>
      <Name>localInSubFunction</Name>
      <IsNull>False</IsNull>
      <AsString>4</AsString>
      <HasExpired>False</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="argument" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>argument</Expression>
      <Name>argument</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="local" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>local</Expression>
      <Name>local</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="@class" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>this.class</Expression>
      <Name>class</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ObjectDump name="localInSubFunction" Type="Value">
      <IsArray exception="Value has expired" />
      <ArrayLenght exception="Value has expired" />
      <ArrayRank exception="Value has expired" />
      <ArrayDimensions exception="Value has expired" />
      <IsObject exception="Value has expired" />
      <IsPrimitive exception="Value has expired" />
      <IsInteger exception="Value has expired" />
      <PrimitiveValue exception="Value has expired" />
      <Expression>localInSubFunction</Expression>
      <Name>localInSubFunction</Name>
      <IsNull exception="Value has expired" />
      <AsString exception="Value has expired" />
      <HasExpired>True</HasExpired>
      <Type>System.Int32</Type>
    </ObjectDump>
    <ProcessExited />
  </Test>
</DebuggerTests>
#endif // EXPECTED_OUTPUT