﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Commands.Test.CloudService.Utilities
{
    using Commands.Utilities.CloudService.AzureTools;
    using Microsoft.WindowsAzure.Commands.Utilities.Common;
    using Microsoft.WindowsAzure.Commands.Utilities.Properties;
    using Moq;
    using Test.Utilities.Common;
    using VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class WAStorageEmulatorTests : TestBase
    {
        [TestMethod]
        public void Start_StorageEmulatorInstalled_UseCorrectCommand()
        {
            // Setup 
            string testFolder = @"c:\foo-bar";
            string expectedCommand = Path.Combine(testFolder, Resources.StorageEmulatorExe);

            WAStorageEmulator emulator = new WAStorageEmulator(@"c:\foo-bar");
            Mock<ProcessHelper> commandRunner = new Mock<ProcessHelper>();
            commandRunner.Setup(p=>p.StartAndWaitForProcess(expectedCommand, Resources.StartStorageEmulatorCommandArgument));
            emulator.CommandRunner = commandRunner.Object;

            // Execute
            emulator.Start();

            // Assert
            commandRunner.Verify();
        }

        [TestMethod]
        public void Stop_StorageEmulatorInstalled_UseCorrectCommand()
        {
            // Setup 
            string testFolder = @"c:\foo-bar";
            string expectedCommand = Path.Combine(testFolder, Resources.StorageEmulatorExe);

            WAStorageEmulator emulator = new WAStorageEmulator(@"c:\foo-bar");
            Mock<ProcessHelper> commandRunner = new Mock<ProcessHelper>();
            commandRunner.Setup(p => p.StartAndWaitForProcess(expectedCommand, Resources.StopStorageEmulatorCommandArgument));
            emulator.CommandRunner = commandRunner.Object;

            // Execute
            emulator.Stop();

            // Assert
            commandRunner.Verify();
        }

        [TestMethod]
        public void Start_StorageEmulatorNotInstalled_GetWarning()
        {
            // Setup 
            WAStorageEmulator emulator = new WAStorageEmulator(null);
            Mock<ProcessHelper> commandRunner = new Mock<ProcessHelper>();

            // Execute
            emulator.Start();

            // Assert
            Assert.AreEqual(Resources.WarningWhenStorageEmulatorIsMissing, emulator.Error);
        }
    }
}