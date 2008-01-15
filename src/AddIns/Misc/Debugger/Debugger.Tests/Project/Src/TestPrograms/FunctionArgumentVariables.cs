// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="David Srbecký" email="dsrbecky@gmail.com"/>
//     <version>$Revision$</version>
// </file>

using System;

namespace Debugger.Tests.TestPrograms
{
	public class FunctionArgumentVariables
	{
		public static void Main()
		{
			System.Diagnostics.Debugger.Break();
			int ref_i = 2;
			int out_i;
			int out_i2;
			string ref_s = "B";
			int? iNull = 5;
			int? iNull_null = null;
			StaticFunction(
				1,
				"A",
				null,
				ref ref_i,
				out out_i,
				out out_i2,
				ref ref_s,
				iNull,
				iNull_null
			);
			VarArgs();
			VarArgs("A");
			VarArgs("A", "B");
			new FunctionArgumentVariables().Function(1, "A");
		}
		
		static void StaticFunction(int i,
		                           string s,
		                           string s_null,
		                           ref int ref_i,
		                           out int out_i,
		                           out int out_i2,
		                           ref string ref_s,
		                           int? iNull,
		                           int? iNull_null)
		{
			out_i = 3;
			System.Diagnostics.Debugger.Break();
			out_i2 = 4;
		}
		
		static void VarArgs(params string[] args)
		{
			System.Diagnostics.Debugger.Break();
		}
		
		void Function(int i, string s)
		{
			System.Diagnostics.Debugger.Break();
		}
	}
}

#if TEST_CODE
namespace Debugger.Tests {
	public partial class DebuggerTests
	{
		[NUnit.Framework.Test]
		public void FunctionArgumentVariables()
		{
			StartTest("FunctionArgumentVariables");
			WaitForPause();
			
			for(int i = 0; i < 5; i++) {
				process.Continue();
				WaitForPause();
				ObjectDump("SelectedStackFrame", process.SelectedStackFrame);
			}
			
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
  <Test name="FunctionArgumentVariables">
    <ProcessStarted />
    <ModuleLoaded symbols="False">mscorlib.dll</ModuleLoaded>
    <ModuleLoaded symbols="True">FunctionArgumentVariables.exe</ModuleLoaded>
    <DebuggingPaused>Break</DebuggingPaused>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="SelectedStackFrame" Type="StackFrame">
      <MethodInfo Type="MethodInfo">
        <Name>StaticFunction</Name>
        <FullName>Debugger.Tests.TestPrograms.FunctionArgumentVariables.StaticFunction</FullName>
        <IsPrivate>True</IsPrivate>
        <IsPublic>False</IsPublic>
        <IsSpecialName>False</IsSpecialName>
        <IsStatic>True</IsStatic>
        <Module>FunctionArgumentVariables.exe</Module>
        <DeclaringType>Debugger.Tests.TestPrograms.FunctionArgumentVariables</DeclaringType>
      </MethodInfo>
      <HasSymbols>True</HasSymbols>
      <HasExpired>False</HasExpired>
      <NextStatement>Start=51,4 End=51,40</NextStatement>
      <ThisValue exception="Static method does not have 'this'." />
      <ContaingClassVariables Type="ValueCollection">
        <Count>0</Count>
      </ContaingClassVariables>
      <ArgumentCount>9</ArgumentCount>
      <Arguments Type="ValueCollection">
        <Count>9</Count>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>False</IsObject>
          <IsPrimitive>True</IsPrimitive>
          <IsInteger>True</IsInteger>
          <PrimitiveValue>1</PrimitiveValue>
          <Expression>i</Expression>
          <Name>i</Name>
          <IsNull>False</IsNull>
          <AsString>1</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.Int32</Type>
        </Item>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>False</IsObject>
          <IsPrimitive>True</IsPrimitive>
          <IsInteger>False</IsInteger>
          <PrimitiveValue>A</PrimitiveValue>
          <Expression>s</Expression>
          <Name>s</Name>
          <IsNull>False</IsNull>
          <AsString>A</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.String</Type>
        </Item>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>False</IsObject>
          <IsPrimitive>False</IsPrimitive>
          <IsInteger>False</IsInteger>
          <PrimitiveValue exception="Value is not a primitive type" />
          <Expression>s_null</Expression>
          <Name>s_null</Name>
          <IsNull>True</IsNull>
          <AsString>&lt;null&gt;</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.Object</Type>
        </Item>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>False</IsObject>
          <IsPrimitive>True</IsPrimitive>
          <IsInteger>True</IsInteger>
          <PrimitiveValue>2</PrimitiveValue>
          <Expression>ref_i</Expression>
          <Name>ref_i</Name>
          <IsNull>False</IsNull>
          <AsString>2</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.Int32</Type>
        </Item>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>False</IsObject>
          <IsPrimitive>True</IsPrimitive>
          <IsInteger>True</IsInteger>
          <PrimitiveValue>3</PrimitiveValue>
          <Expression>out_i</Expression>
          <Name>out_i</Name>
          <IsNull>False</IsNull>
          <AsString>3</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.Int32</Type>
        </Item>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>False</IsObject>
          <IsPrimitive>True</IsPrimitive>
          <IsInteger>True</IsInteger>
          <PrimitiveValue>0</PrimitiveValue>
          <Expression>out_i2</Expression>
          <Name>out_i2</Name>
          <IsNull>False</IsNull>
          <AsString>0</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.Int32</Type>
        </Item>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>False</IsObject>
          <IsPrimitive>True</IsPrimitive>
          <IsInteger>False</IsInteger>
          <PrimitiveValue>B</PrimitiveValue>
          <Expression>ref_s</Expression>
          <Name>ref_s</Name>
          <IsNull>False</IsNull>
          <AsString>B</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.String</Type>
        </Item>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>True</IsObject>
          <IsPrimitive>False</IsPrimitive>
          <IsInteger>False</IsInteger>
          <PrimitiveValue exception="Value is not a primitive type" />
          <Expression>iNull</Expression>
          <Name>iNull</Name>
          <IsNull>False</IsNull>
          <AsString>{System.Nullable&lt;System.Int32&gt;}</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.Nullable&lt;System.Int32&gt;</Type>
        </Item>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>True</IsObject>
          <IsPrimitive>False</IsPrimitive>
          <IsInteger>False</IsInteger>
          <PrimitiveValue exception="Value is not a primitive type" />
          <Expression>iNull_null</Expression>
          <Name>iNull_null</Name>
          <IsNull>False</IsNull>
          <AsString>{System.Nullable&lt;System.Int32&gt;}</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.Nullable&lt;System.Int32&gt;</Type>
        </Item>
      </Arguments>
      <LocalVariables Type="ValueCollection">
        <Count>0</Count>
      </LocalVariables>
    </ObjectDump>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="SelectedStackFrame" Type="StackFrame">
      <MethodInfo Type="MethodInfo">
        <Name>VarArgs</Name>
        <FullName>Debugger.Tests.TestPrograms.FunctionArgumentVariables.VarArgs</FullName>
        <IsPrivate>True</IsPrivate>
        <IsPublic>False</IsPublic>
        <IsSpecialName>False</IsSpecialName>
        <IsStatic>True</IsStatic>
        <Module>FunctionArgumentVariables.exe</Module>
        <DeclaringType>Debugger.Tests.TestPrograms.FunctionArgumentVariables</DeclaringType>
      </MethodInfo>
      <HasSymbols>True</HasSymbols>
      <HasExpired>False</HasExpired>
      <NextStatement>Start=57,4 End=57,40</NextStatement>
      <ThisValue exception="Static method does not have 'this'." />
      <ContaingClassVariables Type="ValueCollection">
        <Count>0</Count>
      </ContaingClassVariables>
      <ArgumentCount>1</ArgumentCount>
      <Arguments Type="ValueCollection">
        <Count>1</Count>
        <Item Type="Value">
          <IsArray>True</IsArray>
          <ArrayLenght>0</ArrayLenght>
          <ArrayRank>1</ArrayRank>
          <ArrayDimensions>[0]</ArrayDimensions>
          <IsObject>False</IsObject>
          <IsPrimitive>False</IsPrimitive>
          <IsInteger>False</IsInteger>
          <PrimitiveValue exception="Value is not a primitive type" />
          <Expression>args</Expression>
          <Name>args</Name>
          <IsNull>False</IsNull>
          <AsString>{System.String[]}</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.String[]</Type>
        </Item>
      </Arguments>
      <LocalVariables Type="ValueCollection">
        <Count>0</Count>
      </LocalVariables>
    </ObjectDump>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="SelectedStackFrame" Type="StackFrame">
      <MethodInfo Type="MethodInfo">
        <Name>VarArgs</Name>
        <FullName>Debugger.Tests.TestPrograms.FunctionArgumentVariables.VarArgs</FullName>
        <IsPrivate>True</IsPrivate>
        <IsPublic>False</IsPublic>
        <IsSpecialName>False</IsSpecialName>
        <IsStatic>True</IsStatic>
        <Module>FunctionArgumentVariables.exe</Module>
        <DeclaringType>Debugger.Tests.TestPrograms.FunctionArgumentVariables</DeclaringType>
      </MethodInfo>
      <HasSymbols>True</HasSymbols>
      <HasExpired>False</HasExpired>
      <NextStatement>Start=57,4 End=57,40</NextStatement>
      <ThisValue exception="Static method does not have 'this'." />
      <ContaingClassVariables Type="ValueCollection">
        <Count>0</Count>
      </ContaingClassVariables>
      <ArgumentCount>1</ArgumentCount>
      <Arguments Type="ValueCollection">
        <Count>1</Count>
        <Item Type="Value">
          <IsArray>True</IsArray>
          <ArrayLenght>1</ArrayLenght>
          <ArrayRank>1</ArrayRank>
          <ArrayDimensions>[1]</ArrayDimensions>
          <IsObject>False</IsObject>
          <IsPrimitive>False</IsPrimitive>
          <IsInteger>False</IsInteger>
          <PrimitiveValue exception="Value is not a primitive type" />
          <Expression>args</Expression>
          <Name>args</Name>
          <IsNull>False</IsNull>
          <AsString>{System.String[]}</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.String[]</Type>
        </Item>
      </Arguments>
      <LocalVariables Type="ValueCollection">
        <Count>0</Count>
      </LocalVariables>
    </ObjectDump>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="SelectedStackFrame" Type="StackFrame">
      <MethodInfo Type="MethodInfo">
        <Name>VarArgs</Name>
        <FullName>Debugger.Tests.TestPrograms.FunctionArgumentVariables.VarArgs</FullName>
        <IsPrivate>True</IsPrivate>
        <IsPublic>False</IsPublic>
        <IsSpecialName>False</IsSpecialName>
        <IsStatic>True</IsStatic>
        <Module>FunctionArgumentVariables.exe</Module>
        <DeclaringType>Debugger.Tests.TestPrograms.FunctionArgumentVariables</DeclaringType>
      </MethodInfo>
      <HasSymbols>True</HasSymbols>
      <HasExpired>False</HasExpired>
      <NextStatement>Start=57,4 End=57,40</NextStatement>
      <ThisValue exception="Static method does not have 'this'." />
      <ContaingClassVariables Type="ValueCollection">
        <Count>0</Count>
      </ContaingClassVariables>
      <ArgumentCount>1</ArgumentCount>
      <Arguments Type="ValueCollection">
        <Count>1</Count>
        <Item Type="Value">
          <IsArray>True</IsArray>
          <ArrayLenght>2</ArrayLenght>
          <ArrayRank>1</ArrayRank>
          <ArrayDimensions>[2]</ArrayDimensions>
          <IsObject>False</IsObject>
          <IsPrimitive>False</IsPrimitive>
          <IsInteger>False</IsInteger>
          <PrimitiveValue exception="Value is not a primitive type" />
          <Expression>args</Expression>
          <Name>args</Name>
          <IsNull>False</IsNull>
          <AsString>{System.String[]}</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.String[]</Type>
        </Item>
      </Arguments>
      <LocalVariables Type="ValueCollection">
        <Count>0</Count>
      </LocalVariables>
    </ObjectDump>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="SelectedStackFrame" Type="StackFrame">
      <MethodInfo Type="MethodInfo">
        <Name>Function</Name>
        <FullName>Debugger.Tests.TestPrograms.FunctionArgumentVariables.Function</FullName>
        <IsPrivate>True</IsPrivate>
        <IsPublic>False</IsPublic>
        <IsSpecialName>False</IsSpecialName>
        <IsStatic>False</IsStatic>
        <Module>FunctionArgumentVariables.exe</Module>
        <DeclaringType>Debugger.Tests.TestPrograms.FunctionArgumentVariables</DeclaringType>
      </MethodInfo>
      <HasSymbols>True</HasSymbols>
      <HasExpired>False</HasExpired>
      <NextStatement>Start=62,4 End=62,40</NextStatement>
      <ThisValue Type="Value">
        <IsArray>False</IsArray>
        <ArrayLenght exception="Value is not an array" />
        <ArrayRank exception="Value is not an array" />
        <ArrayDimensions exception="Value is not an array" />
        <IsObject>True</IsObject>
        <IsPrimitive>False</IsPrimitive>
        <IsInteger>False</IsInteger>
        <PrimitiveValue exception="Value is not a primitive type" />
        <Expression>this</Expression>
        <Name>this</Name>
        <IsNull>False</IsNull>
        <AsString>{Debugger.Tests.TestPrograms.FunctionArgumentVariables}</AsString>
        <HasExpired>False</HasExpired>
        <Type>Debugger.Tests.TestPrograms.FunctionArgumentVariables</Type>
      </ThisValue>
      <ContaingClassVariables Type="ValueCollection">
        <Count>0</Count>
      </ContaingClassVariables>
      <ArgumentCount>2</ArgumentCount>
      <Arguments Type="ValueCollection">
        <Count>2</Count>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>False</IsObject>
          <IsPrimitive>True</IsPrimitive>
          <IsInteger>True</IsInteger>
          <PrimitiveValue>1</PrimitiveValue>
          <Expression>i</Expression>
          <Name>i</Name>
          <IsNull>False</IsNull>
          <AsString>1</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.Int32</Type>
        </Item>
        <Item Type="Value">
          <IsArray>False</IsArray>
          <ArrayLenght exception="Value is not an array" />
          <ArrayRank exception="Value is not an array" />
          <ArrayDimensions exception="Value is not an array" />
          <IsObject>False</IsObject>
          <IsPrimitive>True</IsPrimitive>
          <IsInteger>False</IsInteger>
          <PrimitiveValue>A</PrimitiveValue>
          <Expression>s</Expression>
          <Name>s</Name>
          <IsNull>False</IsNull>
          <AsString>A</AsString>
          <HasExpired>False</HasExpired>
          <Type>System.String</Type>
        </Item>
      </Arguments>
      <LocalVariables Type="ValueCollection">
        <Count>0</Count>
      </LocalVariables>
    </ObjectDump>
    <ProcessExited />
  </Test>
</DebuggerTests>
#endif // EXPECTED_OUTPUT