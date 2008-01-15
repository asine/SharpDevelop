﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="David Srbecký" email="dsrbecky@gmail.com"/>
//     <version>$Revision$</version>
// </file>

using System;

namespace Debugger.Tests
{
	public struct ValueType
	{
		public static void Main()
		{
			new ValueType().Fun();
		}
		
		public void Fun()
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
		public void ValueType()
		{
			StartTest("ValueType");
			WaitForPause();
			
			ObjectDump("this", process.SelectedStackFrame.ThisValue);
			ObjectDump("typeof(this)", process.SelectedStackFrame.ThisValue.Type);
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
  <Test name="ValueType">
    <ProcessStarted />
    <ModuleLoaded symbols="False">mscorlib.dll</ModuleLoaded>
    <ModuleLoaded symbols="True">ValueType.exe</ModuleLoaded>
    <DebuggingPaused>Break</DebuggingPaused>
    <ObjectDump name="this" Type="Value">
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
      <AsString>{Debugger.Tests.ValueType}</AsString>
      <HasExpired>False</HasExpired>
      <Type>Debugger.Tests.ValueType</Type>
    </ObjectDump>
    <ObjectDump name="typeof(this)" Type="DebugType">
      <ManagedType>null</ManagedType>
      <Module Type="Module">
        <Unloaded>False</Unloaded>
        <SymReader>Debugger.Wrappers.CorSym.ISymUnmanagedReader</SymReader>
        <CorModule>Debugger.Wrappers.CorDebug.ICorDebugModule</CorModule>
        <BaseAdress>4194304</BaseAdress>
        <IsDynamic>False</IsDynamic>
        <IsInMemory>False</IsInMemory>
        <Filename>ValueType.exe</Filename>
        <SymbolsLoaded>True</SymbolsLoaded>
        <OrderOfLoading>1</OrderOfLoading>
      </Module>
      <MetadataToken>33554434</MetadataToken>
      <FullName>Debugger.Tests.ValueType</FullName>
      <HasElementType>False</HasElementType>
      <IsArray>False</IsArray>
      <IsGenericType>False</IsGenericType>
      <IsClass>False</IsClass>
      <IsValueType>True</IsValueType>
      <IsPrimitive>False</IsPrimitive>
      <IsInteger>False</IsInteger>
      <BaseType Type="DebugType">
        <ManagedType>null</ManagedType>
        <Module Type="Module">
          <Unloaded>False</Unloaded>
          <SymReader>null</SymReader>
          <CorModule>Debugger.Wrappers.CorDebug.ICorDebugModule</CorModule>
          <BaseAdress>2030829568</BaseAdress>
          <IsDynamic>False</IsDynamic>
          <IsInMemory>False</IsInMemory>
          <Filename>mscorlib.dll</Filename>
          <SymbolsLoaded>False</SymbolsLoaded>
          <OrderOfLoading>0</OrderOfLoading>
        </Module>
        <MetadataToken>33554446</MetadataToken>
        <FullName>System.ValueType</FullName>
        <HasElementType>False</HasElementType>
        <IsArray>False</IsArray>
        <IsGenericType>False</IsGenericType>
        <IsClass>True</IsClass>
        <IsValueType>False</IsValueType>
        <IsPrimitive>False</IsPrimitive>
        <IsInteger>False</IsInteger>
        <BaseType Type="DebugType">
          <ManagedType>null</ManagedType>
          <Module Type="Module">
            <Unloaded>False</Unloaded>
            <SymReader>null</SymReader>
            <CorModule>Debugger.Wrappers.CorDebug.ICorDebugModule</CorModule>
            <BaseAdress>2030829568</BaseAdress>
            <IsDynamic>False</IsDynamic>
            <IsInMemory>False</IsInMemory>
            <Filename>mscorlib.dll</Filename>
            <SymbolsLoaded>False</SymbolsLoaded>
            <OrderOfLoading>0</OrderOfLoading>
          </Module>
          <MetadataToken>33554434</MetadataToken>
          <FullName>System.Object</FullName>
          <HasElementType>False</HasElementType>
          <IsArray>False</IsArray>
          <IsGenericType>False</IsGenericType>
          <IsClass>True</IsClass>
          <IsValueType>False</IsValueType>
          <IsPrimitive>False</IsPrimitive>
          <IsInteger>False</IsInteger>
          <BaseType>null</BaseType>
        </BaseType>
      </BaseType>
    </ObjectDump>
    <ProcessExited />
  </Test>
</DebuggerTests>
#endif // EXPECTED_OUTPUT