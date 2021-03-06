using System;
using System.Collections.Generic;
using Wasmtime.Exports;

namespace Wasmtime.Externs
{
    /// <summary>
    /// Represents an external WebAssembly function.
    /// </summary>
    public class ExternFunction : IImportable
    {
        internal ExternFunction(FunctionExport export, IntPtr func)
        {
            _export = export;
            _func = func;
        }

        /// <summary>
        /// The name of the WebAssembly function.
        /// </summary>
        public string Name => _export.Name;

        /// <summary>
        /// The parameters of the WebAssembly function.
        /// </summary>
        public IReadOnlyList<ValueKind> Parameters => _export.Parameters;

        /// <summary>
        /// The results of the WebAssembly function.
        /// </summary>
        public IReadOnlyList<ValueKind> Results => _export.Results;

        /// <summary>
        /// Invokes the WebAssembly function.
        /// </summary>
        /// <param name="arguments">The array of arguments to pass to the function.</param>
        /// <returns>
        ///   Returns null if the function has no return value.
        ///   Returns the value if the function returns a single value.
        ///   Returns an array of values if the function returns more than one value.
        /// </returns>
        public object? Invoke(params object?[] arguments)
        {
            return Function.Invoke(_func, Parameters, Results, arguments);
        }

        // TODO: remove overload when https://github.com/dotnet/csharplang/issues/1757 is resolved
        /// <summary>
        /// Invokes the WebAssembly function.
        /// </summary>
        /// <param name="arguments">The array of arguments to pass to the function.</param>
        /// <returns>
        ///   Returns null if the function has no return value.
        ///   Returns the value if the function returns a single value.
        ///   Returns an array of values if the function returns more than one value.
        /// </returns>
        public object? Invoke(ReadOnlySpan<object?> arguments)
        {
            return Function.Invoke(_func, Parameters, Results, arguments);
        }

        IntPtr IImportable.GetHandle()
        {
            return Interop.wasm_func_as_extern(_func);
        }

        private FunctionExport _export;
        private IntPtr _func;
    }
}
