﻿using CoreIdentity_1.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace CoreIdentity_1.CustomTagHelpers
{
    [HtmlTargetElement("getUserInfo")]
    public class CustomRoleTagHelper : TagHelper
    {
        readonly UserManager<AppUser> _userManager;

        public CustomRoleTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public int UserID { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string html = "";
            //AppUser appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == UserID);
            IList<string> userRoles = await _userManager.GetRolesAsync(await _userManager.Users.FirstOrDefaultAsync(x=>x.Id==UserID));
            foreach (string role in userRoles)
            {
                html += $"{role},";
            }
            html = html.TrimEnd(',');
            output.Content.SetHtmlContent(html);
        }



    }
}
