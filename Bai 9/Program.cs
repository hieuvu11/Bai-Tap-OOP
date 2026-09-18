using System;

namespace bai9
{
    // 1. Lớp trừu tượng gốc PhuongTien
    public abstract class PhuongTien
    {
        public string HangSanXuat { get; set; }
        public int NamSanXuat { get; set; }
        public double VanTocToiDa { get; set; }

        public PhuongTien(string hangSanXuat, int namSanXuat, double vanTocToiDa)
        {
            HangSanXuat = hangSanXuat;
            NamSanXuat = namSanXuat;
            VanTocToiDa = vanTocToiDa;
        }

        // Các phương thức trừu tượng và ảo
        public abstract double LayVanToc();
        public abstract string LayLoaiNhienLieu();

        public virtual void HienThiThongTin()
        {
            Console.WriteLine($"Hãng sản xuất: {HangSanXuat} | Năm sản xuất: {NamSanXuat} | Vận tốc tối đa: {VanTocToiDa} km/h");
        }
    }

    // 2. Lớp XeDap kế thừa từ PhuongTien
    public class XeDap : PhuongTien
    {
        public XeDap(string hangSanXuat, int namSanXuat, double vanTocToiDa)
            : base(hangSanXuat, namSanXuat, vanTocToiDa) { }

        public override double LayVanToc() => VanTocToiDa;

        public override string LayLoaiNhienLieu() => "Sức người (Không dùng nhiên liệu)";

        public override void HienThiThongTin()
        {
            Console.Write("[Xe Đạp] ");
            base.HienThiThongTin();
            Console.WriteLine($"  -> Nhiên liệu: {LayLoaiNhienLieu()}");
        }
    }

    // 3. Lớp XeMay kế thừa từ PhuongTien
    public class XeMay : PhuongTien
    {
        public string DungTichXilanh { get; set; }

        public XeMay(string hangSanXuat, int namSanXuat, double vanTocToiDa, string dungTichXilanh)
            : base(hangSanXuat, namSanXuat, vanTocToiDa)
        {
            DungTichXilanh = dungTichXilanh;
        }

        public override double LayVanToc() => VanTocToiDa;

        public override string LayLoaiNhienLieu() => "Xăng";

        public override void HienThiThongTin()
        {
            Console.Write("[Xe Máy] ");
            base.HienThiThongTin();
            Console.WriteLine($"  -> Dung tích xi-lanh: {DungTichXilanh} | Nhiên liệu: {LayLoaiNhienLieu()}");
        }
    }

    // 4. Lớp XeOToKhach kế thừa từ PhuongTien
    public class XeOToKhach : PhuongTien
    {
        public int SoChoNgoi { get; set; }

        public XeOToKhach(string hangSanXuat, int namSanXuat, double vanTocToiDa, int soChoNgoi)
            : base(hangSanXuat, namSanXuat, vanTocToiDa)
        {
            SoChoNgoi = soChoNgoi;
        }

        public override double LayVanToc() => VanTocToiDa;

        public override string LayLoaiNhienLieu() => "Dầu Diesel / Xăng";

        public override void HienThiThongTin()
        {
            Console.Write("[Ô tô khách] ");
            base.HienThiThongTin();
            Console.WriteLine($"  -> Số chỗ ngồi: {SoChoNgoi} | Nhiên liệu: {LayLoaiNhienLieu()}");
        }
    }

    // 5. Lớp XeOToTai kế thừa từ PhuongTien
    public class XeOToTai : PhuongTien
    {
        public double TrongTai { get; set; } // Đơn vị: tấn

        public XeOToTai(string hangSanXuat, int namSanXuat, double vanTocToiDa, double trongTai)
            : base(hangSanXuat, namSanXuat, vanTocToiDa)
        {
            TrongTai = trongTai;
        }

        public override double LayVanToc() => VanTocToiDa;

        public override string LayLoaiNhienLieu() => "Dầu Diesel";

        public override void HienThiThongTin()
        {
            Console.Write("[Ô tô tải] ");
            base.HienThiThongTin();
            Console.WriteLine($"  -> Trọng tải: {TrongTai} tấn | Nhiên liệu: {LayLoaiNhienLieu()}");
        }
    }

    // 6. Lớp Program quản lý chạy chương trình
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Sử dụng mảng kiểu lớp cha PhuongTien thể hiện tính đa hình
            PhuongTien[] danhSachPhuongTien = new PhuongTien[]
            {
                new XeDap("Giant", 2023, 30),
                new XeMay("Honda", 2022, 110, "125cc"),
                new XeOToKhach("Hyundai", 2021, 150, 45),
                new XeOToTai("Isuzu", 2020, 120, 8.5)
            };

            Console.WriteLine("=== DANH SÁCH QUẢN LÝ PHƯƠNG TIỆN GIAO THÔNG ===\n");

            foreach (var pt in danhSachPhuongTien)
            {
                pt.HienThiThongTin();
                Console.WriteLine(new string('-', 60));
            }

            Console.ReadLine();
        }
    }
}