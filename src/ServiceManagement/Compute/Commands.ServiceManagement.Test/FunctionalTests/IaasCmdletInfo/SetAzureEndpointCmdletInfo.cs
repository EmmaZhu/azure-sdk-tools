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

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.Test.FunctionalTests.IaasCmdletInfo
{
    using ConfigDataInfo;
    using PowershellCore;

    public class SetAzureEndpointCmdletInfo : CmdletsInfo
    {
        public SetAzureEndpointCmdletInfo()
        {
            this.cmdletName = Utilities.SetAzureEndpointCmdletName;
        }

        public SetAzureEndpointCmdletInfo(AzureEndPointConfigInfo endPointConfig)
        {
            this.cmdletName = Utilities.SetAzureEndpointCmdletName;

            this.cmdletParams.Add(new CmdletParam("Name", endPointConfig.EndpointName));
            this.cmdletParams.Add(new CmdletParam("LocalPort", endPointConfig.EndpointLocalPort));
            if (endPointConfig.EndpointPublicPort.HasValue)
            {
                this.cmdletParams.Add(new CmdletParam("PublicPort", endPointConfig.EndpointPublicPort));
            }
            this.cmdletParams.Add(new CmdletParam("Protocol", endPointConfig.EndpointProtocol.ToString()));
            this.cmdletParams.Add(new CmdletParam("VM", endPointConfig.Vm));
            if (endPointConfig.Acl != null)
            {
                this.cmdletParams.Add(new CmdletParam("ACL", endPointConfig.Acl));
            }
            if (endPointConfig.DirectServerReturn)
            {
                this.cmdletParams.Add(new CmdletParam("DirectServerReturn", endPointConfig.DirectServerReturn));
            }
        }
    }
}
