using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.WindowsAPICodePack.Dialogs;
using TextureEditor.Peg;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace TextureEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private PegFile _peg;
        private CommonOpenFileDialog _fileBrowser;
        private CommonOpenFileDialog _importFileBrowser;

        public MainWindow()
        {
            InitializeComponent();

            _fileBrowser = new CommonOpenFileDialog();
            _fileBrowser.Filters.Add(new CommonFileDialogFilter("Peg files", "*.cpeg_pc;*.cvbm_pc"));

            _importFileBrowser = new CommonOpenFileDialog();
            _importFileBrowser.Filters.Add(new CommonFileDialogFilter("Image file", "*.png")); //Todo: Test and support more formats
        }

        private void OpenTextureFile_OnClick(object sender, RoutedEventArgs e)
        {
            _fileBrowser.IsFolderPicker = false;
            if (_fileBrowser.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string cpuFilePath = _fileBrowser.FileName;
                if (!File.Exists(cpuFilePath))
                {
                    return;
                }

                var cpuFileInfo = new FileInfo(cpuFilePath);

                if (cpuFileInfo.Extension == ".cpeg_pc")
                {
                    var gpuFileInfo = new FileInfo(cpuFileInfo.DirectoryName + "\\" + Path.GetFileNameWithoutExtension(cpuFilePath) + ".gpeg_pc");
                    _peg = new PegFile(cpuFilePath, gpuFileInfo.FullName);
                    _peg.Read();
                }
                else if(cpuFileInfo.Extension == ".cvbm_pc")
                {
                    var gpuFileInfo = new FileInfo(cpuFileInfo.DirectoryName + "\\" + Path.GetFileNameWithoutExtension(cpuFilePath) + ".gvbm_pc");
                    _peg = new PegFile(cpuFilePath, gpuFileInfo.FullName);
                    _peg.Read();
                }
                else
                {
                    throw new Exception($"{cpuFileInfo.Extension} is an invalid file extension for peg files!");
                }

                UpdateWindowTitle();
                PopulateTreeView();
                SetSelectedTexture(0);
            }
        }

        private void SaveChanges_OnClick(object sender, RoutedEventArgs e)
        {
            _peg.Write();
            MessageBox.Show($"Saved changes to:\n{_peg.cpuFileName}, and\n{_peg.gpuFileName}");
        }

        private void ImportSelectedTexture_OnClick(object sender, RoutedEventArgs e)
        {
            if (_peg != null && _peg.Entries.Count > 0 && GetSelectedTextureIndex() > -1)
            {
                _importFileBrowser.IsFolderPicker = false;
                if (_importFileBrowser.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var selectedEntry = _peg.Entries[GetSelectedTextureIndex()];
                    selectedEntry.Bitmap = new Bitmap(_importFileBrowser.FileName);
                    selectedEntry.Edited = true;
                    selectedEntry.RawData = Util.ConvertBitmapToByteArray(selectedEntry.Bitmap);

                    uint original_width = selectedEntry.width;

                    selectedEntry.width = (ushort)selectedEntry.Bitmap.Width;
                    selectedEntry.height = (ushort)selectedEntry.Bitmap.Height;
                    selectedEntry.source_height = (ushort)selectedEntry.Bitmap.Height;

                    //source_width sometimes equals width and sometimes equals 36352. This is a quick hack for now until that behavior is understood.
                    //Not properly setting this causes the game to improperly scale the texture
                    if (selectedEntry.source_width == original_width) 
                    {
                        selectedEntry.source_width = selectedEntry.width;
                    }
                    else
                    {
                        selectedEntry.source_width = 36352;
                    }

                    UpdateCurrentTextureBitmap();
                }
            }
        }

        private void ExtractSelectedTexture_OnClick(object sender, RoutedEventArgs e)
        {
            if (_peg != null && _peg.Entries.Count > 0 && GetSelectedTextureIndex() > -1)
            {
                _fileBrowser.IsFolderPicker = true;
                if (_fileBrowser.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var selectedEntry = _peg.Entries[GetSelectedTextureIndex()];
                    selectedEntry.Bitmap.Save(_fileBrowser.FileName + "\\" + Path.GetFileNameWithoutExtension(selectedEntry.Name) + ".png", ImageFormat.Png);
                }
            }
        }

        private void ExtractAllTextures_OnClick(object sender, RoutedEventArgs e)
        {
            if (_peg != null && _peg.Entries.Count > 0 && GetSelectedTextureIndex() > -1)
            {
                _fileBrowser.IsFolderPicker = true;
                if (_fileBrowser.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    foreach (var entry in _peg.Entries)
                    {
                        entry.Bitmap.Save(_fileBrowser.FileName + "\\" + Path.GetFileNameWithoutExtension(entry.Name) + ".png", ImageFormat.Png);
                    }
                }
            }
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private ImageBrush _checkeredImageBrush;
        private void SolidBackground_OnClick(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).IsChecked)
            {
                _checkeredImageBrush = (ImageBrush)ImageViewBorder.Background;
                ImageViewBorder.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/RFG Texture Editor Redux;component/Images/Solid.png")))
                {
                    TileMode = TileMode.Tile,
                    ViewboxUnits = BrushMappingMode.Absolute,
                    Viewport = new Rect(0, 0, 20, 20)
                };
            }
            else
            {
                ImageViewBorder.Background = _checkeredImageBrush;
            }
        }

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "RFG Texture Editor Redux\n\nA rewrite of the texture editor originally written by 0luke0\n\ngithub repo: https://github.com/Moneyl/RFG-Texture-Editor-Redux\n\nThanks to Gibbed for his work on reversing the peg format and writing the original extraction code.",
                "RFG Texture Editor Redux");
        }

        private void TextureTree_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SetSelectedTexture(GetSelectedTextureIndex());
        }

        private void UpdateWindowTitle()
        {
            Title = $"RFG Texture Editor Redux | {_peg.cpuFileName}, {_peg.Entries.Count} textures";
        }

        private void PopulateTreeView()
        {
            TextureTree.Items.Clear();
            
            foreach (var entry in _peg.Entries)
            {
                TextureTree.Items.Add(new TreeViewItem()
                {
                    Header = entry.Name
                });
            }
        }

        private void SetSelectedTexture(int index)
        {
            if (_peg != null)
            {
                if (index < _peg.Entries.Count && index > -1)
                {
                    TextureView.Source = Util.BitmapToBitmapImage(_peg.Entries[index].Bitmap);
                    ((TreeViewItem)TextureTree.Items[index]).IsSelected = true;
                }
            }
        }

        int GetSelectedTextureIndex()
        {
            return TextureTree.Items.IndexOf(TextureTree.SelectedItem);
        }

        void UpdateCurrentTextureBitmap()
        {
            SetSelectedTexture(GetSelectedTextureIndex());
        }
    }
}
