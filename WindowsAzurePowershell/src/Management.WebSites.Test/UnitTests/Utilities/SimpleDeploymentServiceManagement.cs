﻿// ----------------------------------------------------------------------------------
//
// Copyright 2011 Microsoft Corporation
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

namespace Microsoft.WindowsAzure.Management.Websites.Test.UnitTests.Utilities
{
    using System;
    using Management.Test.Tests.Utilities;
    using VisualStudio.TestTools.UnitTesting;
    using Websites.Services;
    using Websites.Services.DeploymentEntities;
    
    /// <summary>
    /// Simple implementation of the <see cref="IDeploymentServiceManagement"/> interface that can be
    /// used for mocking basic interactions without involving Azure directly.
    /// </summary>
    public class SimpleDeploymentServiceManagement : IDeploymentServiceManagement
    {
        /// <summary>
        /// Gets or sets a value indicating whether the thunk wrappers will
        /// throw an exception if the thunk is not implemented.  This is useful
        /// when debugging a test.
        /// </summary>
        public bool ThrowsIfNotImplemented { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleDeploymentServiceManagement"/> class.
        /// </summary>
        public SimpleDeploymentServiceManagement()
        {
            ThrowsIfNotImplemented = true;
        }

        #region Implementation Thunks

        #region GetDeployments

        public Func<SimpleServiceManagementAsyncResult, Deployments> GetDeploymentsThunk { get; set; }

        public IAsyncResult BeginGetDeployments(int maxItems, AsyncCallback callback, object state)
        {
            SimpleServiceManagementAsyncResult result = new SimpleServiceManagementAsyncResult();
            result.Values["maxItems"] = maxItems;
            result.Values["callback"] = callback;
            result.Values["state"] = state;
            return result;
        }

        public Deployments EndGetDeployments(IAsyncResult asyncResult)
        {
            if (GetDeploymentsThunk != null)
            {
                SimpleServiceManagementAsyncResult result = asyncResult as SimpleServiceManagementAsyncResult;
                Assert.IsNotNull(result, "asyncResult was not SimpleDeploymentServiceManagementAsyncResult!");

                return GetDeploymentsThunk(result);
            }
            else if (ThrowsIfNotImplemented)
            {
                throw new NotImplementedException("GetDeploymentsThunk is not implemented!");
            }

            return default(Deployments);
        }

        #endregion

        #endregion
    }
}

