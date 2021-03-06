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

namespace Microsoft.WindowsAzure.Commands.TrafficManager.Profile
{
    using Microsoft.WindowsAzure.Commands.TrafficManager.Utilities;
    using Microsoft.WindowsAzure.Management.TrafficManager.Models;
    using System.Management.Automation;

    [Cmdlet(VerbsLifecycle.Disable, "AzureTrafficManagerProfile"), OutputType(typeof(bool))]
    public class DisableAzureTrafficManagerProfile : TrafficManagerBaseCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        public override void ExecuteCmdlet()
        {
            TrafficManagerClient.UpdateProfileStatus(Name, ProfileDefinitionStatus.Disabled);
            
            if (PassThru.IsPresent)
            {
                WriteObject(true);
            }
        }
    }
}
