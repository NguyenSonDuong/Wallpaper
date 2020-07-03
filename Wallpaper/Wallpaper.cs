using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wallpaper
{
    class Wallpaper
    {
        // hàm này để lấy thư viện user32 của win
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SystemParametersInfo(uint action, uint param, string path, uint type); // đây là hàm được lấy ra trong thư viện user32.dll
        // Action ở đây là hành động bạn muốn làm: ở đây hành động mk muốn làm là set ảnh cho desktop
        // param: mà mk cx k rõ lắm về biến này
        // path: đường dẫn ảnh tren máy bạn
        // type: mà mk cx k rõ lắm về biến này luôn
        // ==================================================================
        /*Ok haha 
         * Bây giờ ta sẽ khai báo tiếp 2 hàm đăng ký HotKey và hủy đăng ký
        */
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hd, int id, int first, int second);
        /*
         Thi thoảng mk hay quên 
         Ở đây hd: Là Handle của ứng dụng của bạn (Con trỏ)
         id: ID của hotkey bạn đăng ký
         first: là phím đầu tiên ( Shidt, ctrl ... )
         second: là phím phụ ( A,B,F1, Insert...)
             */
        // còn hàm này là để hủy đăng ký 
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hd, int id);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hd, int type);

        const uint SET_DESKTOP_WALLPAPER = 0x14;
        const uint FILE = 0x01;
        // ok đã chạy vậy là mk đã có thể đổi ảnh desktop
        public static void SetDesktopWallpaper(String path)
        {
            SystemParametersInfo(SET_DESKTOP_WALLPAPER, 0, path, FILE);
        }
    }
}
