// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
namespace Linn.Common.Configuration.Extensions
{
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.FileProviders;

    /// <summary>
    /// Represents an EMV file as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class EnvConfigurationSource : FileConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="EnvConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>A <see cref="EnvConfigurationProvider"/></returns>
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EnvConfigurationProvider(this);
        }

        public void ResolvePathAndFileProvider()
        {
            if (this.FileProvider == null && !string.IsNullOrEmpty(this.Path))
            {
                if (!System.IO.Path.IsPathRooted(this.Path))
                {
                    this.Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), this.Path);
                }

                var directory = System.IO.Path.GetDirectoryName(this.Path);

                this.FileProvider = new PhysicalFileProvider(directory);

                this.Path = System.IO.Path.GetFileName(this.Path);
            }
        }
    }
}
