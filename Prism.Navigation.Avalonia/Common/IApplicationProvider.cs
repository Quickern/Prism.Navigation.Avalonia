﻿using Pipopolam.Avalonia.NavigationPages;
using Prism.Navigation.Avalonia.Xaml;

namespace Prism.Common
{
    /// <summary>
    /// Defines a contract for providing Application components.
    /// </summary>
    public interface IApplicationProvider
    {
        /// <summary>
        /// Gets or sets the main page of the Application.
        /// </summary>
        Page MainPage { get; set; }
    }
}
