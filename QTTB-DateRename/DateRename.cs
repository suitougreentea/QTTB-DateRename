using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;
using QTPlugin;

namespace QTTB_DateRename
{
    [PluginAttribute(PluginType.Interactive, Author = "suitougreentea", Name = "DateRename", Version = "1.0.0.0", Description = "超整理法式リネームプラグイン")]
    public class DateRename : IBarDropButton
    {
        const int MODE_NOW = 0;
        const int MODE_CREATED = 1;
        const int MODE_REFRESHED = 2;
        const int MODE_REVERT = -1;

        private IPluginServer pluginServer;

        private ToolStripMenuItem menuItemNow, menuItemCreated, menuItemRefreshed, menuItemRevert;

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
            menuItemRevert = new ToolStripMenuItem("日時を取り除く");
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
            if (item == menuItemNow) RenameFiles(MODE_NOW);
            else if (item == menuItemCreated) RenameFiles(MODE_CREATED);
            else if (item == menuItemRefreshed) RenameFiles(MODE_REFRESHED);
            else if (item == menuItemRevert) RenameFiles(MODE_REVERT);
            return;
        }

        public void OnDropDownOpening(ToolStripDropDownMenu menu)
        {
            menu.Items.Add(menuItemNow);
            menu.Items.Add(menuItemCreated);
            menu.Items.Add(menuItemRefreshed);
            menu.Items.Add(menuItemRevert);
        }

        private void RenameFiles(int mode)
        {
            DateTime date;
            String header = "!!!!!!";
            if(mode == MODE_NOW){
                date = DateTime.Now;
                header = date.ToString("yyMMdd");
            }
            List<string> lstPaths = new List<string>();
            Address[] addresses;
            if (pluginServer.TryGetSelection(out addresses))
            {
                foreach (Address ad in addresses)
                {
                    if (!String.IsNullOrEmpty(ad.Path) && File.Exists(ad.Path))
                    {
                        try
                        {
                            if (mode == MODE_REVERT)
                            {
                                if (Regex.IsMatch(Path.GetFileName(ad.Path), "^[0-9]{6,6}_"))
                                {
                                    File.Move(ad.Path, Path.Combine(Path.GetDirectoryName(ad.Path), Path.GetFileName(ad.Path).Substring(7)));
                                }
                            }
                            else
                            {
                                if (mode == MODE_CREATED)
                                {
                                    date = File.GetCreationTime(ad.Path);
                                    header = date.ToString("yyMMdd");
                                }
                                else if (mode == MODE_REFRESHED)
                                {
                                    date = File.GetLastWriteTime(ad.Path);
                                    header = date.ToString("yyMMdd");
                                }
                                File.Move(ad.Path, Path.Combine(Path.GetDirectoryName(ad.Path), header + "_" + Path.GetFileName(ad.Path)));
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
            }
        }
    }
}
