using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QTPlugin;

namespace QTTB_DateRename
{
    [PluginAttribute(PluginType.Interactive, Author = "suitougreentea", Name = "DateRename", Version = "1.0.0.0", Description = "超整理法式リネームプラグイン")]
    public class DateRename : IBarDropButton
    {
        private IPluginServer pluginServer;

        private ToolStripMenuItem menuItemNow, menuItemCreated, menuItemRefreshed;

        public System.Drawing.Image GetImage(bool fLarge)
        {
            return fLarge ? Resource.ButtonLarge : Resource.ButtonSmall;
        }

        public void InitializeItem()
        {
        }

        public void OnButtonClick()
        {
            throw new NotImplementedException();
        }

        public bool ShowTextLabel
        {
            get { return false; }
        }

        public string Text
        {
            get { return "超整理法リネーム"; }
        }

        public void Close(EndCode endCode)
        {
            return;
        }

        public bool HasOption
        {
            get { return false; }
        }

        public void OnMenuItemClick(MenuType menuType, string menuText, ITab tab)
        {
            throw new NotImplementedException();
        }

        public void OnOption()
        {
            return;
        }

        public void OnShortcutKeyPressed(int index)
        {
            return;
        }

        public void Open(IPluginServer pluginServer, QTPlugin.Interop.IShellBrowser shellBrowser)
        {
            this.pluginServer = pluginServer;

            menuItemNow = new ToolStripMenuItem("現在の日時");
            menuItemCreated = new ToolStripMenuItem("作成日時");
            menuItemRefreshed = new ToolStripMenuItem("更新日時");
        }

        public bool QueryShortcutKeys(out string[] actions)
        {
            actions = new string[] { "1番目のショートカットキー", "2番目のショートカットキー" };
            return true;
        }

        public bool IsSplitButton
        {
            get { return false; }
        }

        public void OnDropDownItemClick(ToolStripItem item, MouseButtons mouseButton)
        {
            if (item == menuItemNow) throw new NotImplementedException();
            return;
        }

        public void OnDropDownOpening(ToolStripDropDownMenu menu)
        {
            menu.Items.Add(menuItemNow);
            menu.Items.Add(menuItemCreated);
            menu.Items.Add(menuItemRefreshed);
        }

        private void RenameFiles(DateTime date)
        {
            List<string> lstPaths = new List<string>();
            Address[] addresses;
            if (pluginServer.TryGetSelection(out addresses))
            {
                foreach (Address ad in addresses)
                {
                    if (!String.IsNullOrEmpty(ad.Path))
                        lstPaths.Add(ad.Path);
                }
            }
        }
    }
}
