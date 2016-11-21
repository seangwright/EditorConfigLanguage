﻿using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.DragDrop;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace EditorConfig
{
    [Export(typeof(IDropHandlerProvider))]
    [DropFormat("CF_VSSTGPROJECTITEMS")]
    [DropFormat("FileDrop")]
    [Name("EditorConfigDropHandler")]
    [ContentType(ContentTypes.EditorConfig)]
    [Order(Before = "DefaultFileDropHandler")]
    internal class EditorConfigDropHandlerProvider : IDropHandlerProvider
    {
        [Import(typeof(ITextBufferUndoManagerProvider))]
        private ITextBufferUndoManagerProvider UndoProvider { get; set; }

        public IDropHandler GetAssociatedDropHandler(IWpfTextView view)
        {
            var undoManager = UndoProvider.GetTextBufferUndoManager(view.TextBuffer);

            return view.Properties.GetOrCreateSingletonProperty(() => new EditorConfigDropHandler(view, undoManager));
        }
    }
}