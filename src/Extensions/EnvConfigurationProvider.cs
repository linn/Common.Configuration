// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
namespace Linn.Common.Configuration.Extensions
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// An ENV file based <see cref="FileConfigurationProvider"/>.
    /// </summary>
    public class EnvConfigurationProvider : FileConfigurationProvider
    {
        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public EnvConfigurationProvider(FileConfigurationSource source)
            : base(source)
        {
        }

        /// <summary>
        /// Loads the ENV data from a stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public override void Load(Stream stream)
        {
            using var streamReader = new StreamReader(stream);
            this.Data = ReadNonEmptyLines(streamReader).Select(ParseEnvFileLine)
                .ToDictionary(v => v.Key, x => x.Value)!;
        }

        private static IEnumerable<string> ReadNonEmptyLines(TextReader reader)
        {
            var line = reader.ReadLine();

            while (line != null)
            {
                if (line.Length > 0)
                {
                    yield return line;
                }

                line = reader.ReadLine();
            }
        }

        private static KeyValuePair<string, string> ParseEnvFileLine(string value)
        {
            var split = value.Split(new[] { '=' }, 2);

            return split.Length > 1 ? new KeyValuePair<string, string>(split[0], split[1]) 
                       : new KeyValuePair<string, string>(split[0], string.Empty);
        }
    }
}