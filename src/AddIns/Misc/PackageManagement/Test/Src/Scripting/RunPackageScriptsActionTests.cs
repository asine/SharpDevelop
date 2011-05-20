﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.PackageManagement.Design;
using ICSharpCode.PackageManagement.Scripting;
using NuGet;
using NUnit.Framework;
using PackageManagement.Tests.Helpers;

namespace PackageManagement.Tests.Scripting
{
	[TestFixture]
	public class RunPackageScriptsActionTests
	{
		RunPackageScriptsAction action;
		FakePackageManagementProject fakeProject;
		FakePackageScriptFactory fakeScriptFactory;
		FakePackageScriptSession fakeSession;
			
		void CreateAction()
		{
			fakeProject = new FakePackageManagementProject();
			fakeSession = new FakePackageScriptSession();
			fakeScriptFactory = new FakePackageScriptFactory();
			action = new RunPackageScriptsAction(fakeProject, fakeSession, fakeScriptFactory);
		}
		
		PackageOperationEventArgs CreatePackageOperationEventArgs()
		{
			return CreatePackageOperationEventArgs(@"d:\projects\myproject\packages\test");
		}
		
		PackageOperationEventArgs CreatePackageOperationEventArgs(string installPath)
		{
			var package = new FakePackage("Test");
			string targetPath = @"d:\projects\myproject\packages\target";
			return new PackageOperationEventArgs(package, targetPath, installPath);
		}
		
		[Test]
		public void Constructor_PackageIsInstalled_PackageInitScriptIsRun()
		{
			CreateAction();
			var eventArgs = CreatePackageOperationEventArgs();
			fakeProject.FirePackageInstalledEvent(eventArgs);
			
			IPackageScriptSession session = fakeScriptFactory.FirstPackageInitializeScriptCreated.SessionPassedToExecute;
			FakePackageScriptSession expectedSession = fakeSession;
			
			Assert.AreEqual(expectedSession, session);
		}
		
		[Test]
		public void Constructor_PackageIsInstalled_PackageInitScriptIsCreated()
		{
			CreateAction();
			var eventArgs = CreatePackageOperationEventArgs(@"d:\projects\myproject\packages\test");

			fakeProject.FirePackageInstalledEvent(eventArgs);
			
			string path = fakeScriptFactory.FirstPackageInstallDirectoryPassed;
			
			Assert.AreEqual(@"d:\projects\myproject\packages\test", path);
		}
		
		[Test]
		public void Dispose_PackageIsInstalled_PackageInitScriptIsNotRun()
		{
			CreateAction();
			action.Dispose();
			
			var eventArgs = CreatePackageOperationEventArgs();
			fakeProject.FirePackageInstalledEvent(eventArgs);
			
			int count = fakeScriptFactory.FakePackageInstallScriptsCreated.Count;
			
			Assert.AreEqual(0, count);
		}
		
		[Test]
		public void Constructor_PackageReferenceIsAdded_PackageInstallScriptIsRun()
		{
			CreateAction();
			var eventArgs = CreatePackageOperationEventArgs();
			fakeProject.FirePackageReferenceAddedEvent(eventArgs);
			
			IPackageScriptSession session = fakeScriptFactory.FirstPackageInstallScriptCreated.SessionPassedToExecute;
			FakePackageScriptSession expectedSession = fakeSession;
			
			Assert.AreEqual(expectedSession, session);
		}
		
		[Test]
		public void Constructor_PackageReferenceIsAdded_PackageInstallScriptIsCreated()
		{
			CreateAction();
			var eventArgs = CreatePackageOperationEventArgs(@"d:\projects\myproject\packages\test");
			fakeProject.FirePackageReferenceAddedEvent(eventArgs);
			
			string path = fakeScriptFactory.FirstPackageInstallDirectoryPassed;
			
			Assert.AreEqual(@"d:\projects\myproject\packages\test", path);
		}
		
		[Test]
		public void Dispose_PackageReferenceIsAdded_PackageInstallScriptIsNotRun()
		{
			CreateAction();
			action.Dispose();
			
			var eventArgs = CreatePackageOperationEventArgs();
			fakeProject.FirePackageReferenceAddedEvent(eventArgs);
			
			int count = fakeScriptFactory.FakePackageInstallScriptsCreated.Count;
			
			Assert.AreEqual(0, count);
		}
		
		[Test]
		public void Constructor_PackageReferenceIsAdded_InstallScriptIsPassedProject()
		{
			CreateAction();
			var eventArgs = CreatePackageOperationEventArgs();
			fakeProject.FirePackageReferenceAddedEvent(eventArgs);
			
			var project = fakeScriptFactory.FirstPackageInstallScriptCreated.Project;
			
			Assert.AreEqual(fakeProject, project);
		}
		
		[Test]
		public void Constructor_PackageReferenceIsRemoved_PackageUninstallScriptIsRun()
		{
			CreateAction();
			var eventArgs = CreatePackageOperationEventArgs();
			fakeProject.FirePackageReferenceRemovedEvent(eventArgs);
			
			IPackageScriptSession session = fakeScriptFactory.FirstPackageUninstallScriptCreated.SessionPassedToExecute;
			FakePackageScriptSession expectedSession = fakeSession;
			
			Assert.AreEqual(expectedSession, session);
		}
		
		[Test]
		public void Constructor_PackageReferenceIsRemoved_PackageUninstallScriptIsCreated()
		{
			CreateAction();
			var eventArgs = CreatePackageOperationEventArgs(@"d:\projects\myproject\packages\test");
			fakeProject.FirePackageReferenceRemovedEvent(eventArgs);
			
			string path = fakeScriptFactory.FirstPackageInstallDirectoryPassed;
			
			Assert.AreEqual(@"d:\projects\myproject\packages\test", path);
		}
		
		[Test]
		public void Dispose_PackageReferenceIsRemoved_PackageUninstallScriptIsNotRun()
		{
			CreateAction();
			action.Dispose();
			
			var eventArgs = CreatePackageOperationEventArgs();
			fakeProject.FirePackageReferenceRemovedEvent(eventArgs);
			
			int count = fakeScriptFactory.FakePackageUninstallScriptsCreated.Count;
			
			Assert.AreEqual(0, count);
		}
		
		[Test]
		public void Constructor_PackageReferenceIsRemoved_UninstallScriptIsPassedProject()
		{
			CreateAction();
			var eventArgs = CreatePackageOperationEventArgs();
			fakeProject.FirePackageReferenceRemovedEvent(eventArgs);
			
			var project = fakeScriptFactory.FirstPackageUninstallScriptCreated.Project;
			
			Assert.AreEqual(fakeProject, project);
		}
	}
}
