﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.PackageManagement.Design;
using ICSharpCode.PackageManagement.Scripting;
using NUnit.Framework;
using PackageManagement.Tests.Helpers;

namespace PackageManagement.Tests.Scripting
{
	[TestFixture]
	public class PackageInitializeScriptTests
	{
		PackageInitializeScript script;
		FakePackageScriptSession fakeSession;
		FakePackageScriptFileName fakeScriptFileName;
		
		void CreateScript()
		{
			fakeScriptFileName = new FakePackageScriptFileName();			
			fakeSession = new FakePackageScriptSession();
			script = new PackageInitializeScript(fakeScriptFileName);
		}
		
		void AssertSessionVariableIsRemoved(string variableName)
		{
			bool removed = fakeSession.VariablesRemoved.Contains(variableName);
			Assert.IsTrue(removed);			
		}
		
		void ExecuteScript()
		{
			script.Execute(fakeSession);
		}
		
		[Test]
		public void Execute_ExistingEnvironmentPathIsEmptyString_PathToScriptAddedToEnvironmentPath()
		{
			CreateScript();
			fakeScriptFileName.GetScriptDirectoryReturnValue = @"d:\projects\myproject\packages\test\tools";
			fakeSession.SetEnvironmentPath(String.Empty);
			
			ExecuteScript();
			
			string actualEnvironmentPath = fakeSession.GetEnvironmentPath();
			string expectedEnvironmentPath = @"d:\projects\myproject\packages\test\tools";
			
			Assert.AreEqual(expectedEnvironmentPath, actualEnvironmentPath);
		}
		
		[Test]
		public void Execute_OneItemInOriginalEnvironmentPath_PathToScriptAppendedToEnvironmentPath()
		{
			CreateScript();
			fakeScriptFileName.GetScriptDirectoryReturnValue = @"d:\projects\myproject\packages\test\tools";
			fakeSession.SetEnvironmentPath(@"c:\users\sharpdevelop\ps;");
			
			ExecuteScript();
			
			string actualEnvironmentPath = fakeSession.GetEnvironmentPath();
			string expectedEnvironmentPath = @"c:\users\sharpdevelop\ps;d:\projects\myproject\packages\test\tools";
			
			Assert.AreEqual(expectedEnvironmentPath, actualEnvironmentPath);
		}
		
		[Test]
		public void Execute_OneItemInOriginalEnvironmentPathMissingSemiColonAtEnd_PathToScriptAppendedToEnvironmentPathWithSemiColonAtStart()
		{
			CreateScript();
			fakeScriptFileName.GetScriptDirectoryReturnValue = @"d:\projects\myproject\packages\test\tools";
			fakeSession.SetEnvironmentPath(@"c:\users\sharpdevelop\ps");
			
			ExecuteScript();
			
			string actualEnvironmentPath = fakeSession.GetEnvironmentPath();
			string expectedEnvironmentPath = @"c:\users\sharpdevelop\ps;d:\projects\myproject\packages\test\tools";
			
			Assert.AreEqual(expectedEnvironmentPath, actualEnvironmentPath);
		}
		
		[Test]
		public void Execute_OriginalEnvironmentPathIsNull_PathToScriptAppendedToEnvironmentPath()
		{
			CreateScript();
			fakeScriptFileName.GetScriptDirectoryReturnValue = @"d:\projects\myproject\packages\test\tools";
			fakeSession.SetEnvironmentPath(null);
			
			ExecuteScript();
			
			string actualEnvironmentPath = fakeSession.GetEnvironmentPath();
			string expectedEnvironmentPath = @"d:\projects\myproject\packages\test\tools";
			
			Assert.AreEqual(expectedEnvironmentPath, actualEnvironmentPath);
		}
		
		[Test]
		public void Execute_ScriptDirectoryDoesNotExist_PathIsNotUpdated()
		{
			CreateScript();
			fakeScriptFileName.GetScriptDirectoryReturnValue = @"d:\projects\myproject\packages\test\tools";
			fakeScriptFileName.ScriptDirectoryExistsReturnValue = false;
			fakeSession.SetEnvironmentPath(String.Empty);
			
			ExecuteScript();
			
			string actualEnvironmentPath = fakeSession.GetEnvironmentPath();
			string expectedEnvironmentPath = String.Empty;
			
			Assert.AreEqual(expectedEnvironmentPath, actualEnvironmentPath);
		}
		
		[Test]
		public void Execute_PackageIsSet_PackageSessionVariableIsSet()
		{
			CreateScript();
			var expectedPackage = new FakePackage("Test");
			script.Package = expectedPackage;
			ExecuteScript();
			
			var actualPackage = fakeSession.VariablesAdded["__package"];
			
			Assert.AreEqual(expectedPackage, actualPackage);
		}
		
		[Test]
		public void Execute_PackageInstallDirectoryIsSet_RootPathSessionVariableIsSet()
		{
			CreateScript();
			string expectedRootPath = @"d:\projects\myproject\packages\test";
			fakeScriptFileName.PackageInstallDirectory = expectedRootPath;
			ExecuteScript();
			
			var rootPath = fakeSession.VariablesAdded["__rootPath"];
			
			Assert.AreEqual(expectedRootPath, rootPath);
		}
		
		[Test]
		public void Execute_PackageInstallDirectoryIsSet_ToolsPathSessionVariableIsSet()
		{
			CreateScript();
			fakeScriptFileName.GetScriptDirectoryReturnValue = @"d:\projects\myproject\packages\test\tools";
			ExecuteScript();
			
			var toolsPath = fakeSession.VariablesAdded["__toolsPath"];
			string expectedToolsPath = @"d:\projects\myproject\packages\test\tools";
			
			Assert.AreEqual(expectedToolsPath, toolsPath);
		}
		
		[Test]
		public void Execute_PackageInstallDirectoryIsSet_ProjectSessionVariableIsSet()
		{
			CreateScript();
			ExecuteScript();
			
			var project = fakeSession.VariablesAdded["__project"];
			
			Assert.IsNull(project);
		}
		
		[Test]
		public void Execute_PackageInstallDirectoryIsSet_ScriptIsInvoked()
		{
			CreateScript();
			fakeScriptFileName.ToStringReturnValue = @"d:\projects\myproject\packages\test\tools\init.ps1";
			ExecuteScript();
			
			string actualScript = fakeSession.ScriptPassedToInvokeScript;
			
			string expectedScript = 
				"& 'd:\\projects\\myproject\\packages\\test\\tools\\init.ps1' $__rootPath $__toolsPath $__package $__project";
			
			Assert.AreEqual(expectedScript, actualScript);
		}
		
		[Test]
		public void Execute_PackageInstallDirectoryIsSet_PackageSessionVariableIsRemoved()
		{
			CreateScript();
			ExecuteScript();
			
			AssertSessionVariableIsRemoved("__package");
		}
		
		[Test]
		public void Execute_PackageInstallDirectoryIsSet_RootPathSessionVariableIsRemoved()
		{
			CreateScript();
			ExecuteScript();
			
			AssertSessionVariableIsRemoved("__rootPath");
		}
		
		[Test]
		public void Execute_PackageInstallDirectoryIsSet_ToolsPathSessionVariableIsRemoved()
		{
			CreateScript();
			ExecuteScript();
			
			AssertSessionVariableIsRemoved("__toolsPath");
		}
		
		[Test]
		public void Execute_PackageInstallDirectoryIsSet_ProjectSessionVariableIsRemoved()
		{
			CreateScript();
			ExecuteScript();
			
			AssertSessionVariableIsRemoved("__project");
		}
	}
}
