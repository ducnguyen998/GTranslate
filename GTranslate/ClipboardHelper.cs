using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK.Libraries.SharpClipboardNS;

namespace GTranslate
{
    public class ClipboardHelper
    {
        private readonly SharpClipboard sharpClipboard;

        private readonly MainWindowViewmodel viewer;

        public ClipboardHelper(IServiceProvider serviceProvider)
        {
            viewer = serviceProvider.GetRequiredService<MainWindowViewmodel>();
            sharpClipboard = new SharpClipboard();
            sharpClipboard.ClipboardChanged += ClipboardChanged;
        }

        private void ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            // Is the content copied of text type?
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                // Get the cut/copied text.
                this.viewer.TranslateAgent.Input = sharpClipboard.ClipboardText.Replace("\r\n", " ");
            }

            // Is the content copied of image type?
            else if (e.ContentType == SharpClipboard.ContentTypes.Image)
            {
                // Get the cut/copied image.
                // Image img = sharpClipboard.ClipboardImage;
            }

            // Is the content copied of file type?
            else if (e.ContentType == SharpClipboard.ContentTypes.Files)
            {
                // Get the cut/copied file/files.
                Debug.WriteLine(sharpClipboard.ClipboardFiles.ToArray());

                // ...or use 'ClipboardFile' to get a single copied file.
                Debug.WriteLine(sharpClipboard.ClipboardFile);
            }

            // If the cut/copied content is complex, use 'Other'.
            else if (e.ContentType == SharpClipboard.ContentTypes.Other)
            {
                // Do something with 'clipboard.ClipboardObject' or 'e.Content' here...
            }
        }
    }
}
