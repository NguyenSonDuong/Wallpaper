using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wallpaper
{
    public partial class Form1 : Form
    {
        // đây là mã của các phím điều khiển
        enum KeyMod
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        
        }
        // mk sẽ tạo 2 biến để lưu đường dẫn ảnh macdinh là ảnh crush của mk
        // còn chuyen là ảnh mk muốn mn nhìn thấy
        string macdinh;
        string chuyen;
        // biến này để lưu trạng thái chuyển ảnh của mk
        bool isChange = false;
        public Form1()
        {
            InitializeComponent();
            
            // giờ mk sẽ đăng ký hotkey khi mà ứng dụng đk mở
            Wallpaper.RegisterHotKey(this.Handle, 1999, (int)KeyMod.Control, Keys.F7.GetHashCode());
            // mk sẽ đăng ký thêm 2 HotKey để ẩn và hiện ứng dụng
            Wallpaper.RegisterHotKey(this.Handle, 2000, (int)KeyMod.Shift, Keys.Home.GetHashCode());
            Wallpaper.RegisterHotKey(this.Handle, 2001, (int)KeyMod.Shift, Keys.End.GetHashCode());
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            // đây mà mã message của sự kiện nhấn HotKey mà bạn đăng ký
            if(m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                if(id == 1999)
                {
                    if (isChange)
                    {
                        // isChange là true tức đã được chuyển thì ta sẽ chuyển lại về mặc định
                        // khi mà người dùng nhấn phím tắt
                        Wallpaper.SetDesktopWallpaper(macdinh);
                        isChange = false;
                    }
                    else
                    {
                        // như thế nhưng ngược lại
                        Wallpaper.SetDesktopWallpaper(chuyen);
                        isChange = true;
                    }
                }else if(id == 2000)
                {
                    // 0 là ẩn 1 là hiện
                    Wallpaper.ShowWindow(this.Handle, 0);
                }else if(id == 2001) {
                    Wallpaper.ShowWindow(this.Handle, 1);
                }
                // ok có vẻ đã ổn nhưng mk sẽ chỉnh thêm 1 tẹo
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                macdinh = open.FileName;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                chuyen = open.FileName;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Wallpaper.UnregisterHotKey(this.Handle, 1999);
            Wallpaper.UnregisterHotKey(this.Handle, 2000);
            Wallpaper.UnregisterHotKey(this.Handle, 2001);
            File.WriteAllText("path.ini", macdinh + "\n" + chuyen);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // dòng code này để giúp ứng dụng bạn khởi động cùng windown
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",true);
            if (checkBox1.Checked)
            {
                reg.SetValue("ApplicationName", Application.ExecutablePath);
            }else
            {
                reg.DeleteValue("ApplicationName");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                String[] output = File.ReadAllLines("path.ini");
                macdinh = output[0];
                chuyen = output[1];
                // nếu khởi động mà đọc đk file thì ta sẽ ẩn ứng dụng đi
                //ohhh k hoạt động
                Wallpaper.ShowWindow(this.Handle, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng chọn file ảnh");
            }
        }
    }
}
