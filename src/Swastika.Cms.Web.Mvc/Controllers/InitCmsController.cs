﻿// Licensed to the Swastika I/O Foundation under one or more agreements.
// The Swastika I/O Foundation licenses this file to you under the GNU General Public License v3.0 license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Swastika.Cms.Lib.Services;
using Swastika.Cms.Mvc.Controllers;

namespace Swastika.Cms.Web.Mvc.Controllers
{
    public class InitCmsController : BaseController<InitCmsController>
    {
        public InitCmsController(IHostingEnvironment env) : base(env)
        {
        }

        [HttpGet]
        [Route("")]
        [Route("{culture}")]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(GlobalConfigurationService.Instance.GetConfigConnectionKey()))
            {
                return RedirectToAction("Init", "Portal", new { culture = ROUTE_DEFAULT_CULTURE });
            }
            else
            {
                GlobalConfigurationService.Instance.IsInit = true;
                return RedirectToAction("", "Home", new { culture = CurrentLanguage });
            }
        }
    }
}