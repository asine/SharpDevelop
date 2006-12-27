﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Xml;
using System.IO;
using NUnit.Framework;

namespace ICSharpCode.WpfDesign.XamlDom.Tests
{
	public class TestHelper
	{
		public static T[] ToArray<T>(IEnumerable<T> e)
		{
			return new List<T>(e).ToArray();
		}
		
		public static void TestLoading(string xaml)
		{
			Debug.WriteLine("Load using own XamlParser:");
			ExampleClass.nextUniqueIndex = 0;
			TestHelperLog.logBuilder = new StringBuilder();
			XamlDocument doc = XamlParser.Parse(new XmlTextReader(new StringReader(xaml)));
			Assert.IsNotNull(doc, "doc is null");
			object ownResult = doc.RootInstance;
			string ownLog = TestHelperLog.logBuilder.ToString();
			Assert.IsNotNull(ownResult, "ownResult is null");
			
			Debug.WriteLine("Load using builtin XamlReader:");
			ExampleClass.nextUniqueIndex = 0;
			TestHelperLog.logBuilder = new StringBuilder();
			object officialResult = XamlReader.Load(new XmlTextReader(new StringReader(xaml)));
			string officialLog = TestHelperLog.logBuilder.ToString();
			Assert.IsNotNull(officialResult, "officialResult is null");
			
			TestHelperLog.logBuilder = null;
			// compare:
			string officialSaved = XamlWriter.Save(officialResult);
			string ownSaved = XamlWriter.Save(ownResult);
			
			Assert.AreEqual(officialSaved, ownSaved);
			
			// compare logs:
			Assert.AreEqual(officialLog, ownLog);
		}
	}
	
	internal static class TestHelperLog
	{
		internal static StringBuilder logBuilder;
		
		internal static void Log(string text)
		{
			if (logBuilder != null) {
				logBuilder.AppendLine(text);
				Debug.WriteLine(text);
			}
		}
	}
}
