using Microsoft.Extensions.Configuration;
using RockLib.Configuration.ObjectFactory;
using System.Diagnostics;

namespace RockLib.Diagnostics
{
    /// <summary>
    /// Provides extension methods for <see cref="IConfiguration"/> instances related to diagnostics.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Creates an instance of <see cref="DiagnosticsSettings"/> from the specified configuration.
        /// </summary>
        /// <see cref="EventTypeFilter"/>.
        /// <param name="configuration">
        /// A configuration object that contains the values for a <see cref="DiagnosticsSettings"/> object.
        /// </param>
        /// <returns>
        /// A <see cref="DiagnosticsSettings"/> object created from the values found in the
        /// <paramref name="configuration"/> parameter.
        /// </returns>
        /// <remarks>
        /// This method creates the instance of <see cref="DiagnosticsSettings"/> by applying the
        /// <see cref="ConfigurationObjectFactory.Create{T}(IConfiguration, DefaultTypes, ValueConverters)"/>
        /// extension method to the <paramref name="configuration"/> parameter and passing
        /// it a <see cref="DefaultTypes"/> argument with two mappings: the default type for
        /// the abstract <see cref="TraceListener"/> class is <see cref="DefaultTraceListener"/>,
        /// and the default type for the abstract <see cref="TraceFilter"/> class is
        /// </remarks>
        public static DiagnosticsSettings CreateDiagnosticsSettings(this IConfiguration configuration)
        {
            var defaultTypes = new DefaultTypes
            {
                { typeof(TraceListener), typeof(DefaultTraceListener) },
                { typeof(TraceFilter), typeof(EventTypeFilter) }
            };
            return configuration.Create<DiagnosticsSettings>(defaultTypes);
        }
    }
}
