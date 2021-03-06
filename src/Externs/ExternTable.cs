using System;
using Wasmtime.Exports;

namespace Wasmtime.Externs
{
    /// <summary>
    /// Represents an external WebAssembly table.
    /// </summary>
    public class ExternTable : IImportable
    {
        internal ExternTable(TableExport export, IntPtr table)
        {
            _export = export;
            _table = table;
        }

        IntPtr IImportable.GetHandle()
        {
            return Interop.wasm_table_as_extern(_table);
        }

        /// <summary>
        /// The name of the WebAssembly table.
        /// </summary>
        public string Name => _export.Name;

        /// <summary>
        /// The value kind of the table.
        /// </summary>
        public ValueKind Kind => _export.Kind;

        /// <summary>
        /// The minimum number of elements in the table.
        /// </summary>
        public uint Minimum => _export.Minimum;

        /// <summary>
        /// The maximum number of elements in the table.
        /// </summary>
        public uint Maximum => _export.Maximum;

        private TableExport _export;
        private IntPtr _table;
    }
}
