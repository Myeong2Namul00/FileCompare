namespace FileCompare
{
    public partial class Form1 : Form
    {
        private readonly record struct EntryInfo(bool IsDirectory, long Size, DateTime LastWriteTime);

        public Form1()
        {
            InitializeComponent();
            lvwLeftDir.MultiSelect = false;
            lvwRightDir.MultiSelect = false;
            btnCopyFromLeft.Click += btnCopyFromLeft_Click;
            btnCopyFromRight.Click += btnCopyFromRight_Click;
        }

        private void PopulateListView(ListView lv, string folderPath)
        {
            lv.BeginUpdate();
            lv.Items.Clear();

            try
            {
                var dirs = Directory.EnumerateDirectories(folderPath)
                    .Select(p => new DirectoryInfo(p))
                    .OrderBy(d => d.Name, StringComparer.OrdinalIgnoreCase);

                foreach (var dir in dirs)
                {
                    var directorySummary = GetDirectorySummary(dir.FullName);
                    var item = new ListViewItem(dir.Name);
                    item.SubItems.Add(directorySummary.TotalSize.ToString("N0") + " 바이트");
                    item.SubItems.Add(directorySummary.LastWriteTime.ToString("g"));
                    item.Tag = new EntryInfo(true, directorySummary.TotalSize, directorySummary.LastWriteTime);
                    lv.Items.Add(item);
                }
                foreach (var f in Directory.EnumerateFiles(folderPath)
                    .Select(p => new FileInfo(p))
                    .OrderBy(f => f.Name, StringComparer.OrdinalIgnoreCase))
                {
                    var item = new ListViewItem(f.Name);
                    item.SubItems.Add(f.Length.ToString("N0") + " 바이트");
                    item.SubItems.Add(f.LastWriteTime.ToString("g"));
                    item.Tag = new EntryInfo(false, f.Length, f.LastWriteTime);
                    lv.Items.Add(item);
                }
                for (int i = 0; i < lv.Columns.Count; i++ )
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
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(this, "접근 권한 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lv.EndUpdate();
            }
        }

        private static (long TotalSize, DateTime LastWriteTime) GetDirectorySummary(string directoryPath)
        {
            var options = new EnumerationOptions
            {
                RecurseSubdirectories = true,
                IgnoreInaccessible = true
            };

            var totalSize = 0L;
            var lastWriteTime = Directory.GetLastWriteTime(directoryPath);

            foreach (var filePath in Directory.EnumerateFiles(directoryPath, "*", options))
            {
                var fileInfo = new FileInfo(filePath);
                totalSize += fileInfo.Length;

                if (fileInfo.LastWriteTime > lastWriteTime)
                {
                    lastWriteTime = fileInfo.LastWriteTime;
                }
            }

            foreach (var childDirectory in Directory.EnumerateDirectories(directoryPath, "*", options))
            {
                var childLastWriteTime = Directory.GetLastWriteTime(childDirectory);
                if (childLastWriteTime > lastWriteTime)
                {
                    lastWriteTime = childLastWriteTime;
                }
            }

            return (totalSize, lastWriteTime);
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
                    var isDirectoryItem1 = has1 && item1.Tag is EntryInfo entryInfo1 && entryInfo1.IsDirectory;
                    var isDirectoryItem2 = has2 && item2.Tag is EntryInfo entryInfo2 && entryInfo2.IsDirectory;

                    if (isDirectoryItem1)
                    {
                        item1.Font = new Font(lv1.Font, FontStyle.Bold);
                    }

                    if (isDirectoryItem2)
                    {
                        item2.Font = new Font(lv2.Font, FontStyle.Bold);
                    }

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

        private void btnCopyFromLeft_Click(object? sender, EventArgs e)
        {
            CopySelectedFile(lvwLeftDir, txtLeftDir.Text, txtRightDir.Text);
        }

        private void btnCopyFromRight_Click(object? sender, EventArgs e)
        {
            CopySelectedFile(lvwRightDir, txtRightDir.Text, txtLeftDir.Text);
        }

        private static void CopyDirectoryRecursive(string sourcePath, string destinationPath)
        {
            Directory.CreateDirectory(destinationPath);

            foreach (var dir in Directory.EnumerateDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                var relativePath = Path.GetRelativePath(sourcePath, dir);
                Directory.CreateDirectory(Path.Combine(destinationPath, relativePath));
            }

            foreach (var file in Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories))
            {
                var relativePath = Path.GetRelativePath(sourcePath, file);
                var destinationFilePath = Path.Combine(destinationPath, relativePath);
                var destinationFolderPath = Path.GetDirectoryName(destinationFilePath);

                if (String.IsNullOrWhiteSpace(destinationFolderPath) == false)
                {
                    Directory.CreateDirectory(destinationFolderPath);
                }

                File.Copy(file, destinationFilePath, true);
            }

            var allDirectories = Directory.EnumerateDirectories(sourcePath, "*", SearchOption.AllDirectories)
                .OrderByDescending(path => path.Count(c => c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar));

            foreach (var sourceDir in allDirectories)
            {
                var relativePath = Path.GetRelativePath(sourcePath, sourceDir);
                var destinationDir = Path.Combine(destinationPath, relativePath);
                Directory.SetLastWriteTime(destinationDir, Directory.GetLastWriteTime(sourceDir));
            }

            Directory.SetLastWriteTime(destinationPath, Directory.GetLastWriteTime(sourcePath));
        }

        private void CopySelectedFile(ListView sourceView, string sourceDir, string destinationDir)
        {
            if (String.IsNullOrWhiteSpace(sourceDir) || String.IsNullOrWhiteSpace(destinationDir))
            {
                MessageBox.Show(this, "양쪽 폴더를 먼저 선택하세요.", "안내",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (sourceView.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "복사할 항목을 선택하세요.", "안내",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selected = sourceView.SelectedItems[0];
            if (String.IsNullOrWhiteSpace(selected.Text) || selected.Tag is not EntryInfo sourceInfo)
            {
                MessageBox.Show(this, "빈 항목은 복사할 수 없습니다.", "안내",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var sourcePath = Path.Combine(sourceDir, selected.Text);
            var destinationPath = Path.Combine(destinationDir, selected.Text);

            try
            {
                if (sourceInfo.IsDirectory)
                {
                    if (Directory.Exists(sourcePath) == false)
                    {
                        MessageBox.Show(this, "원본 폴더를 찾을 수 없습니다.", "오류",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (Directory.Exists(destinationPath))
                    {
                        var sourceSummary = GetDirectorySummary(sourcePath);
                        var destinationSummary = GetDirectorySummary(destinationPath);

                        if (sourceSummary.LastWriteTime < destinationSummary.LastWriteTime)
                        {
                            var result = MessageBox.Show(this,
                                "더 오래된 폴더 내용으로 덮어씌우려 하고 있습니다. 진행하시겠습니까?",
                                "경고",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                            if (result != DialogResult.Yes)
                            {
                                return;
                            }
                        }
                    }

                    CopyDirectoryRecursive(sourcePath, destinationPath);
                }
                else
                {
                    if (File.Exists(sourcePath) == false)
                    {
                        MessageBox.Show(this, "원본 파일을 찾을 수 없습니다.", "오류",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var destinationFolderPath = Path.GetDirectoryName(destinationPath);
                    if (String.IsNullOrWhiteSpace(destinationFolderPath) == false)
                    {
                        Directory.CreateDirectory(destinationFolderPath);
                    }

                    if (File.Exists(destinationPath))
                    {
                        var destinationInfo = new FileInfo(destinationPath);
                        var sourceFileInfo = new FileInfo(sourcePath);

                        if (sourceFileInfo.LastWriteTime < destinationInfo.LastWriteTime)
                        {
                            var result = MessageBox.Show(this,
                                "더 오래된 파일로 덮어씌우려 하고 있습니다. 진행하시겠습니까?",
                                "경고",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                            if (result != DialogResult.Yes)
                            {
                                return;
                            }
                        }
                    }

                    File.Copy(sourcePath, destinationPath, true);
                }

                PopulateListView(lvwLeftDir, txtLeftDir.Text);
                PopulateListView(lvwRightDir, txtRightDir.Text);
                ColorListViewItem(lvwLeftDir, lvwRightDir);
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, "파일 복사 중 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(this, "접근 권한 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
