﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *  .Net Core Plugin Manager is distributed under the GNU General Public License version 3 and  
 *  is also available under alternative licenses negotiated directly with Simon Carter.  
 *  If you obtained Service Manager under the GPL, then the GPL applies to all loadable 
 *  Service Manager modules used on your system as well. The GPL (version 3) is 
 *  available at https://opensource.org/licenses/GPL-3.0
 *
 *  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 *  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 *  See the GNU General Public License for more details.
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2018 - 2020 Simon Carter.  All Rights Reserved.
 *
 *  Product:  AspNetCore.PluginManager.Tests
 *  
 *  File: TestHttpRequest.cs
 *
 *  Purpose:  Mock HttpRequest class
 *
 *  Date        Name                Reason
 *  20/04/2020  Simon Carter        Initially Created
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AspNetCore.PluginManager.Tests
{
    public class TestHttpRequest : HttpRequest
    {
        #region Private Members

        private IRequestCookieCollection _requestCookieCollection;
        private HttpContext _httpContext;
        private IHeaderDictionary _headerDictionary;
        private readonly List<KeyValuePair<string, StringValues>> _queryCollection;
        private readonly QueryString _queryString;

        #endregion Private Members

        #region Constructors

        public TestHttpRequest()
        {
            Path = "/";
            _queryCollection = new List<KeyValuePair<string, StringValues>>();
            _queryString = new QueryString();
        }

        public TestHttpRequest(IRequestCookieCollection cookies)
            : this()
        {
            _requestCookieCollection = cookies ?? throw new ArgumentNullException(nameof(cookies));
        }

        public TestHttpRequest(HttpContext context, IRequestCookieCollection cookies)
            : this(cookies)
        {
            _httpContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Constructors

        #region HttpRequest Methods

        public override Stream Body { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override Int64? ContentLength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override String ContentType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IRequestCookieCollection Cookies
        {
            get
            {
                if (_requestCookieCollection == null)
                    _requestCookieCollection = new TestRequestCookieCollection();

                return _requestCookieCollection;
            }

            set
            {
                _requestCookieCollection = value;
            }
        }
        public override IFormCollection Form { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override Boolean HasFormContentType => throw new NotImplementedException();

        public override IHeaderDictionary Headers
        {
            get
            {
                if (_headerDictionary == null)
                {
                    _headerDictionary = new TestHeaderDictionary();
                    _headerDictionary.Add("User-Agent", "No valid user agent has been set");

                }

                return _headerDictionary;
            }
        }

        public override HostString Host { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override HttpContext HttpContext
        {
            get
            {
                return _httpContext;
            }
        }

        public override Boolean IsHttps { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override String Method { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override PathString Path { get; set; }
        public override PathString PathBase
        {
            get
            {
                return new PathString();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override String Protocol { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override IQueryCollection Query
        {
            get
            {
                return _queryCollection as IQueryCollection;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override QueryString QueryString 
        { 
            get
            {
                return _queryString;
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public override String Scheme { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        #endregion HttpRequest Methods

        #region Public Methods

        public void SetContext(HttpContext context)
        {
            _httpContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Public Methods
    }
}
