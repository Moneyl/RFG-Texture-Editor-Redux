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
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace TextureEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private PegFile Peg;
        public CommonOpenFileDialog FileBrowser;
        public CommonOpenFileDialog ImportFileBrowser;

        public MainWindow()
        {
            InitializeComponent();

            FileBrowser = new CommonOpenFileDialog();
            FileBrowser.Filters.Add(new CommonFileDialogFilter("Peg files", "*.cpeg_pc;*.cvbm_pc"));

            ImportFileBrowser = new CommonOpenFileDialog();
            ImportFileBrowser.Filters.Add(new CommonFileDialogFilter("Image file", "*.png")); //Todo: Test and support more formats
        }

        private void OpenTextureFile_OnClick(object sender, RoutedEventArgs e)
        {
            FileBrowser.IsFolderPicker = false;
            if (FileBrowser.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string cpuFilePath = FileBrowser.FileName;
                if (!File.Exists(cpuFilePath))
                {
                    return;
                }

                var cpuFileInfo = new FileInfo(cpuFilePath);

                if (cpuFileInfo.Extension == ".cpeg_pc")
                {
                    var gpuFileInfo = new FileInfo(cpuFileInfo.DirectoryName + "\\" + Path.GetFileNameWithoutExtension(cpuFilePath) + ".gpeg_pc");
                    Peg = new PegFile(cpuFilePath, gpuFileInfo.FullName); //Create new PegFile instance, constructor calls PegFile.Read()
                }
                else if(cpuFileInfo.Extension == ".cvbm_pc")
                {
                    var gpuFileInfo = new FileInfo(cpuFileInfo.DirectoryName + "\\" + Path.GetFileNameWithoutExtension(cpuFilePath) + ".gvbm_pc");
                    Peg = new PegFile(cpuFilePath, gpuFileInfo.FullName); //Create new PegFile instance, constructor calls PegFile.Read()
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
            Peg.Write();
            MessageBox.Show($"Saved changes to:\n{Peg.cpuFileName}, and\n{Peg.gpuFileName}");
        }

        private void ImportSelectedTexture_OnClick(object sender, RoutedEventArgs e)
        {
            if (Peg != null && Peg.Entries.Count > 0 && GetSelectedTextureIndex() > -1)
            {
                ImportFileBrowser.IsFolderPicker = false;
                if (ImportFileBrowser.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var selectedEntry = Peg.Entries[GetSelectedTextureIndex()];
                    selectedEntry.Bitmap = new Bitmap(ImportFileBrowser.FileName);
                    selectedEntry.Edited = true;
                    selectedEntry.RawData = ConvertBitmapToByteArray(selectedEntry.Bitmap);

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
            if (Peg != null && Peg.Entries.Count > 0 && GetSelectedTextureIndex() > -1)
            {
                FileBrowser.IsFolderPicker = true;
                if (FileBrowser.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var selectedEntry = Peg.Entries[GetSelectedTextureIndex()];
                    selectedEntry.Bitmap.Save(FileBrowser.FileName + "\\" + Path.GetFileNameWithoutExtension(selectedEntry.Name) + ".png", ImageFormat.Png);
                }
            }
        }

        private void ExtractAllTextures_OnClick(object sender, RoutedEventArgs e)
        {
            if (Peg != null && Peg.Entries.Count > 0 && GetSelectedTextureIndex() > -1)
            {
                FileBrowser.IsFolderPicker = true;
                if (FileBrowser.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    foreach (var entry in Peg.Entries)
                    {
                        entry.Bitmap.Save(FileBrowser.FileName + "\\" + Path.GetFileNameWithoutExtension(entry.Name) + ".png", ImageFormat.Png);
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
            Title = $"RFG Texture Editor Redux | {Peg.cpuFileName}, {Peg.Entries.Count} textures";
        }

        private void PopulateTreeView()
        {
            TextureTree.Items.Clear();
            
            foreach (var entry in Peg.Entries)
            {
                TextureTree.Items.Add(new TreeViewItem()
                {
                    Header = entry.Name
                });
            }
        }

        private void SetSelectedTexture(int index)
        {
            if (Peg != null)
            {
                if (index < Peg.Entries.Count && index > -1)
                {
                    TextureView.Source = BitmapToBitmapImage(Peg.Entries[index].Bitmap);
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

        //Todo: Move to helper namespace, look into alternative conversion methods.
        static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png); //Use here to keep transparency
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        static byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
            {
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
                byte[] data = new byte[bitmap.Width * bitmap.Height * 4]; //* 3 for rgb

                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = Marshal.ReadByte(bitmapData.Scan0, i);
                }
                bitmap.UnlockBits(bitmapData);

                //At this point the data is in BGRA arrangement, need to make it RGBA for DXT purposes.
                var redChannel = new byte[data.Length / 4];
                var greenChannel = new byte[data.Length / 4];
                var blueChannel = new byte[data.Length / 4];
                var alphaChannel = new byte[data.Length / 4];

                int pixelIndex = 0;
                for (int i = 0; i < data.Length - 3; i += 4)
                {
                    blueChannel[pixelIndex] = data[i];
                    greenChannel[pixelIndex] = data[i + 1];
                    redChannel[pixelIndex] = data[i + 2];
                    alphaChannel[pixelIndex] = data[i + 3];
                    pixelIndex++;
                }

                pixelIndex = 0;
                for (int i = 0; i < data.Length - 3; i += 4)
                {
                    data[i] = redChannel[pixelIndex];
                    data[i + 1] = greenChannel[pixelIndex];
                    data[i + 2] = blueChannel[pixelIndex];
                    data[i + 3] = alphaChannel[pixelIndex];
                    pixelIndex++;
                }

                return data;
            }
            //else if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
            //{

            //}
            else
            {
                throw new Exception($"Texture import failed! {bitmap.PixelFormat.ToString()} is currently an unsupported import pixel format.");
            }
        }
    }
}
