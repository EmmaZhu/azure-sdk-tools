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

namespace Microsoft.WindowsAzure.Commands.Scheduler
{
    using Microsoft.WindowsAzure.Commands.Utilities.Properties;
    using Microsoft.WindowsAzure.Commands.Utilities.Scheduler.Model;
    using System;
    using System.Linq;
    using System.Management.Automation;
    using Utilities.Websites.Common;

    /// <summary>
    /// Cmdlet to list job collections across all regions or a specific region
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureSchedulerJobCollection"), OutputType(typeof(PSJobCollection))]
    public class GetSchedulerJobCollectionCommand : SchedulerBaseCmdlet
    {
        [Parameter(Position = 0, Mandatory = false, HelpMessage = "The location name.")]
        [ValidateNotNullOrEmpty]
        public string Location { get; set; }

        [Parameter(Position = 1, Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "The job collection name.")]
        [ValidateNotNullOrEmpty]
        public string JobCollectionName { get; set; }

        public override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(Location) && !SMClient.GetAvailableRegions().Contains(Location, StringComparer.OrdinalIgnoreCase))
                throw new Exception(Resources.SchedulerInvalidLocation);
            else
                WriteObject(SMClient.GetJobCollection(region:Location, jobCollection:JobCollectionName), true); 
        }      
    }
}
