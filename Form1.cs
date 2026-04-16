namespace FileCompare
{
    public partial class Form1 : Form
    {
        private readonly record struct EntryInfo(bool IsDirectory, long Size, DateTime LastWriteTime);

        public Form1()
        {
            InitializeComponent();
        }

        private void PopulateListView(ListView lv, string folderPath)
        {
            lv.BeginUpdate();
            lv.Items.Clear();

            try
            {
                var dirs = Directory.EnumerateDirectories(folderPath)
                    .Select(p => new DirectoryInfo(p))
                    .OrderBy(d => d.Name);

                foreach (var dir in dirs)
                {
                    var item = new ListViewItem(dir.Name);
                    item.SubItems.Add("<DIR>");
                    item.SubItems.Add(dir.LastWriteTime.ToString("g"));
                    item.Tag = new EntryInfo(true, 0, dir.LastWriteTime);
                    lv.Items.Add(item);
                }
                foreach (var f in Directory.EnumerateFiles(folderPath)
                    .Select(p => new FileInfo(p))
                    .OrderBy(f => f.Name))
                {
                    var item = new ListViewItem(f.Name);
                    item.SubItems.Add(f.Length.ToString("N0") + " 바이트");
                    item.SubItems.Add(f.LastWriteTime.ToString("g"));
                    item.Tag = new EntryInfo(false, f.Length, f.LastWriteTime);
                    lv.Items.Add(item);
                }
                for (int i = 0; i < lv.Items.Count; i++ )
                {
                    lv.AutoResizeColumn(i,
                        ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(this, "폴더를 찾을 수 없습니다.", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, "입출력 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lv.EndUpdate();
            }
        }

        private static ListViewItem CloneListViewItem(ListViewItem source)
        {
            var cloned = new ListViewItem(source.Text);
            for (int i = 1; i < source.SubItems.Count; i++)
            {
                cloned.SubItems.Add(source.SubItems[i].Text);
            }
            cloned.Tag = source.Tag;
            return cloned;
        }

        private static ListViewItem CreateEmptyItem()
        {
            var item = new ListViewItem("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            return item;
        }

        private void ColorListViewItem(ListView lv1, ListView lv2)
        {
            var map1 = lv1.Items
                .Cast<ListViewItem>()
                .ToDictionary(item => item.Text, StringComparer.OrdinalIgnoreCase);
            var map2 = lv2.Items
                .Cast<ListViewItem>()
                .ToDictionary(item => item.Text, StringComparer.OrdinalIgnoreCase);

            var names = map1.Keys
                .Union(map2.Keys, StringComparer.OrdinalIgnoreCase)
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            lv1.BeginUpdate();
            lv2.BeginUpdate();

            try
            {
                lv1.Items.Clear();
                lv2.Items.Clear();

                foreach (var name in names)
                {
                    var has1 = map1.TryGetValue(name, out var src1);
                    var has2 = map2.TryGetValue(name, out var src2);

                    var item1 = has1 ? CloneListViewItem(src1!) : CreateEmptyItem();
                    var item2 = has2 ? CloneListViewItem(src2!) : CreateEmptyItem();

                    if (has1 && has2 && item1.Tag is EntryInfo info1 && item2.Tag is EntryInfo info2)
                    {
                        if (info1.IsDirectory == info2.IsDirectory &&
                            info1.Size == info2.Size &&
                            info1.LastWriteTime == info2.LastWriteTime)
                        {
                            item1.ForeColor = Color.Black;
                            item2.ForeColor = Color.Black;
                        }
                        else if (info1.LastWriteTime > info2.LastWriteTime)
                        {
                            item1.ForeColor = Color.Red;
                            item2.ForeColor = Color.Blue;
                        }
                        else if (info1.LastWriteTime < info2.LastWriteTime)
                        {
                            item1.ForeColor = Color.Blue;
                            item2.ForeColor = Color.Red;
                        }
                        else
                        {
                            item1.ForeColor = Color.Black;
                            item2.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (has1)
                        {
                            item1.ForeColor = Color.Purple;
                        }
                        if (has2)
                        {
                            item2.ForeColor = Color.Purple;
                        }
                    }

                    lv1.Items.Add(item1);
                    lv2.Items.Add(item2);
                }
            }
            finally
            {
                lv1.EndUpdate();
                lv2.EndUpdate();
            }
        }

        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요";

                if (String.IsNullOrEmpty(txtLeftDir.Text) == false)
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                    PopulateListView(lvwLeftDir, txtLeftDir.Text);
                    ColorListViewItem(lvwLeftDir, lvwRightDir);
                }
            }
        }

        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요";

                if (String.IsNullOrEmpty(txtRightDir.Text) == false)
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                    PopulateListView(lvwRightDir, txtRightDir.Text);
                    ColorListViewItem(lvwLeftDir, lvwRightDir);
                }
            }
        }
    }
}
