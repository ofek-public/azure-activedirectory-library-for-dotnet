﻿//----------------------------------------------------------------------
// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
// Apache License 2.0
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Security.Authentication.Web;
using Microsoft.IdentityModel.Clients.ActiveDirectory.Internal;

namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    /// <summary>
    /// The main class representing the authority issuing tokens for resources.
    /// </summary>
    public sealed partial class AuthenticationContext
    {
        /// <summary>
        /// Gets or Sets flag to enable logged in user authentication. Note that enabling this flag requires some extra application capabilites.
        /// </summary>
        public bool UseCorporateNetwork { get; set; }

        /// <summary>
        /// Acquires security token from the authority.
        /// </summary>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="credential">The credential to use for token acquisition.</param>
        /// <returns>It contains Access Token and the Access Token's expiration time. Refresh Token property will be null for this overload.</returns>
        public IAsyncOperation<AuthenticationResult> AcquireTokenAsync(string resource, string clientId, UserCredential credential)
        {
            return RunTaskAsAsyncOperation(this.AcquireTokenCommonAsync(resource, clientId, credential));
        }

        /// <summary>
        /// Acquires security token from the authority.
        /// </summary>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="credential">The credential to use for token acquisition.</param>
        /// <returns>It contains Access Token and the Access Token's expiration time. Refresh Token property will be null for this overload.</returns>
        internal IAsyncOperation<AuthenticationResult> AcquireTokenAsync(string resource, string clientId, UserAssertion credential)
        {
            return RunTaskAsAsyncOperation(this.AcquireTokenCommonAsync(resource, clientId, credential));
        }

        /// <summary>
        /// Acquires security token from the authority in SSO mode.
        /// </summary>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <returns>It contains Access Token, Refresh Token and the Access Token's expiration time.</returns>
        [DefaultOverload]
        public IAsyncOperation<AuthenticationResult> AcquireTokenAsync(string resource, string clientId)
        {
            return RunTaskAsAsyncOperation(this.AcquireTokenCommonAsync(resource, clientId, WebAuthenticationBroker.GetCurrentApplicationCallbackUri(), PromptBehavior.Auto, UserIdentifier.AnyUser));
        }

        /// <summary>
        /// Acquires security token from the authority.
        /// </summary>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="redirectUri">Address to return to upon receiving a response from the authority.</param>
        /// <returns>It contains Access Token, Refresh Token and the Access Token's expiration time.</returns>
        [DefaultOverload]
        public IAsyncOperation<AuthenticationResult> AcquireTokenAsync(string resource, string clientId, Uri redirectUri)
        {
            return RunTaskAsAsyncOperation(this.AcquireTokenCommonAsync(resource, clientId, GetRedirectUri(redirectUri), PromptBehavior.Auto, UserIdentifier.AnyUser));
        }

        /// <summary>
        /// Acquires security token from the authority.
        /// </summary>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="redirectUri">Address to return to upon receiving a response from the authority.</param>
        /// <param name="promptBehavior">If <see cref="PromptBehavior.Always"/>, asks service to show user the authentication page which gives them chance to authenticate as a different user.</param>
        /// <returns>It contains Access Token, Refresh Token and the Access Token's expiration time.</returns>
        [DefaultOverload]
        public IAsyncOperation<AuthenticationResult> AcquireTokenAsync(string resource, string clientId, Uri redirectUri, PromptBehavior promptBehavior)
        {
            return RunTaskAsAsyncOperation(this.AcquireTokenCommonAsync(resource, clientId, GetRedirectUri(redirectUri), promptBehavior, UserIdentifier.AnyUser));
        }

        /// <summary>
        /// Acquires security token from the authority.
        /// </summary>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="redirectUri">Address to return to upon receiving a response from the authority.</param>
        /// <param name="promptBehavior">If <see cref="PromptBehavior.Always"/>, asks service to show user the authentication page which gives them chance to authenticate as a different user.</param>
        /// <param name="userId">Identifier of the user token is requested for. If created from DisplayableId, this parameter will be used to pre-populate the username field in the authentication form. Please note that the end user can still edit the username field and authenticate as a different user. 
        /// If you want to be notified of such change with an exception, create UserIdentifier with type RequiredDisplayableId. This parameter can be null.</param>
        /// <returns>It contains Access Token, Refresh Token and the Access Token's expiration time.</returns>
        [DefaultOverload]
        public IAsyncOperation<AuthenticationResult> AcquireTokenAsync(string resource, string clientId, Uri redirectUri, PromptBehavior promptBehavior, UserIdentifier userId)
        {
            return RunTaskAsAsyncOperation(this.AcquireTokenCommonAsync(resource, clientId, GetRedirectUri(redirectUri), promptBehavior, userId));
        }

        /// <summary>
        /// Acquires security token from the authority.
        /// </summary>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="redirectUri">Address to return to upon receiving a response from the authority.</param>
        /// <param name="promptBehavior">If <see cref="PromptBehavior.Always"/>, asks service to show user the authentication page which gives them chance to authenticate as a different user.</param>
        /// <param name="userId">Identifier of the user token is requested for. If created from DisplayableId, this parameter will be used to pre-populate the username field in the authentication form. Please note that the end user can still edit the username field and authenticate as a different user. 
        /// If you want to be notified of such change with an exception, create UserIdentifier with type RequiredDisplayableId. This parameter can be null.</param>
        /// <param name="extraQueryParameters">This parameter will be appended as is to the query string in the HTTP authentication request to the authority. The parameter can be null.</param>
        /// <returns>It contains Access Token, Refresh Token and the Access Token's expiration time.</returns>
        public IAsyncOperation<AuthenticationResult> AcquireTokenAsync(string resource, string clientId, Uri redirectUri, PromptBehavior promptBehavior, UserIdentifier userId, string extraQueryParameters)
        {
            return RunTaskAsAsyncOperation(this.AcquireTokenCommonAsync(resource, clientId, redirectUri, promptBehavior, userId, extraQueryParameters));
        }

        /// <summary>
        /// Acquires security token from the authority.
        /// </summary>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="promptBehavior">If <see cref="PromptBehavior.Always"/>, asks service to show user the authentication page which gives them chance to authenticate as a different user.</param>
        /// <returns>It contains Access Token, Refresh Token and the Access Token's expiration time.</returns>
        public IAsyncOperation<AuthenticationResult> AcquireTokenAsync(string resource, string clientId, PromptBehavior promptBehavior)
        {
            return RunTaskAsAsyncOperation(this.AcquireTokenCommonAsync(resource, clientId, WebAuthenticationBroker.GetCurrentApplicationCallbackUri(), promptBehavior, UserIdentifier.AnyUser));
        }

        /// <summary>
        /// Acquires security token from the authority.
        /// </summary>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="promptBehavior">If <see cref="PromptBehavior.Always"/>, asks service to show user the authentication page which gives them chance to authenticate as a different user.</param>
        /// <param name="userId">Identifier of the user token is requested for. If created from DisplayableId, this parameter will be used to pre-populate the username field in the authentication form. Please note that the end user can still edit the username field and authenticate as a different user. 
        /// If you want to be notified of such change with an exception, create UserIdentifier with type RequiredDisplayableId. This parameter can be null.</param>
        /// <returns>It contains Access Token, Refresh Token and the Access Token's expiration time.</returns>
        public IAsyncOperation<AuthenticationResult> AcquireTokenAsync(string resource, string clientId, PromptBehavior promptBehavior, UserIdentifier userId)
        {
            return RunTaskAsAsyncOperation(this.AcquireTokenCommonAsync(resource, clientId, WebAuthenticationBroker.GetCurrentApplicationCallbackUri(), promptBehavior, userId));
        }

        /// <summary>
        /// Acquires security token from the authority.
        /// </summary>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token.</param>
        /// <param name="clientId">Identifier of the client requesting the token.</param>
        /// <param name="promptBehavior">If <see cref="PromptBehavior.Always"/>, asks service to show user the authentication page which gives them chance to authenticate as a different user.</param>
        /// <param name="userId">Identifier of the user token is requested for. If created from DisplayableId, this parameter will be used to pre-populate the username field in the authentication form. Please note that the end user can still edit the username field and authenticate as a different user. 
        /// If you want to be notified of such change with an exception, create UserIdentifier with type RequiredDisplayableId. This parameter can be null.</param>
        /// <param name="extraQueryParameters">This parameter will be appended as is to the query string in the HTTP authentication request to the authority. The parameter can be null.</param>
        /// <returns>It contains Access Token, Refresh Token and the Access Token's expiration time.</returns>
        public IAsyncOperation<AuthenticationResult> AcquireTokenAsync(string resource, string clientId, PromptBehavior promptBehavior, UserIdentifier userId, string extraQueryParameters)
        {
            return RunTaskAsAsyncOperation(this.AcquireTokenCommonAsync(resource, clientId, WebAuthenticationBroker.GetCurrentApplicationCallbackUri(), promptBehavior, userId, extraQueryParameters));
        }

        private IWebUI CreateWebAuthenticationDialog(PromptBehavior promptBehavior)
        {
            return NetworkPlugin.WebUIFactory.Create(promptBehavior, this.UseCorporateNetwork);
        }
    }
}