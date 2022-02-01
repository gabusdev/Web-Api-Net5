﻿using Microsoft.Extensions.Localization;

namespace CustomExceptions
{
    public class InvalidTokenBadRequestException : BaseBadRequestException
    {
        public InvalidTokenBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400009;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}