using System;
using System.Collections.Generic;

namespace QuanLyHangHoa
{
    // 1. LỚP CƠ SỞ TRỪU TƯỢNG: HangHoa
    public abstract class HangHoa
    {
        private string maHang;
        private string tenHang;
        private decimal donGia;
        private int soLuongTon;

        public string MaHang
        {
            get { return maHang; }
            private set { maHang = string.IsNullOrWhiteSpace(value) ? "Mặc định" : value; }
        }

        public string TenHang
        {
            get { return tenHang; }
            set { tenHang = string.IsNullOrWhiteSpace(value) ? "Tên mặc định" : value; }
        }

        public decimal DonGia
        {
            get { return donGia; }
            set { donGia = value >= 0 ? value : 0; }
        }

        public int SoLuongTon
        {
            get { return soLuongTon; }
            set { soLuongTon = value >= 0 ? value : 0; }
        }

        public HangHoa(string maHang, string tenHang, decimal donGia, int soLuongTon)
        {
            MaHang = maHang;
            TenHang = tenHang;
            DonGia = donGia;
            SoLuongTon = soLuongTon;
        }

        public abstract decimal TinhTienVAT();
        public abstract string DanhGiaMucDoBanBuon();

        public override string ToString()
        {
            return $"{MaHang,-10} | {TenHang,-20} | {DonGia,12:N0} | {SoLuongTon,10} | {TinhTienVAT(),12:N0} | {DanhGiaMucDoBanBuon(),-15}";
        }
    }

    // 2. LỚP HÀNG THỰC PHẨM
    public class HangThucPham : HangHoa
    {
        private DateTime ngaySanXuat;
        private DateTime ngayHetHan;

        public string NhaCungCap { get; set; }
        public DateTime NgaySanXuat
        {
            get { return ngaySanXuat; }
            set { ngaySanXuat = value; }
        }
        public DateTime NgayHetHan
        {
            get { return ngayHetHan; }
            set { ngayHetHan = value < ngaySanXuat ? ngaySanXuat : value; } // Ngày hết hạn phải >= Ngày sản xuất
        }

        public HangThucPham(string maHang, string tenHang, decimal donGia, int soLuongTon, string nhaCungCap, DateTime ngaySanXuat, DateTime ngayHetHan)
            : base(maHang, tenHang, donGia, soLuongTon)
        {
            NhaCungCap = nhaCungCap;
            NgaySanXuat = ngaySanXuat;
            NgayHetHan = ngayHetHan;
        }

        public override decimal TinhTienVAT()
        {
            return DonGia * 0.05m; // VAT 5%
        }

        public override string DanhGiaMucDoBanBuon()
        {
            if (SoLuongTon > 0 && NgayHetHan < DateTime.Now)
                return "Khó bán";
            return "Không đánh giá";
        }

        public override string ToString()
        {
            return base.ToString() + $" | NCC: {NhaCungCap} (HSD: {NgayHetHan:dd/MM/yyyy})";
        }
    }

    // 3. LỚP HÀNG ĐIỆN MÁY
    public class HangDienMay : HangHoa
    {
        public int ThoiGianBaoHanh { get; set; } // Tính theo tháng
        public decimal CongSuat { get; set; } // Tính theo KW

        public HangDienMay(string maHang, string tenHang, decimal donGia, int soLuongTon, int thoiGianBaoHanh, decimal congSuat)
            : base(maHang, tenHang, donGia, soLuongTon)
        {
            ThoiGianBaoHanh = thoiGianBaoHanh >= 0 ? thoiGianBaoHanh : 0;
            CongSuat = congSuat >= 0 ? congSuat : 0;
        }

        public override decimal TinhTienVAT()
        {
            return DonGia * 0.10m; // VAT 10%
        }

        public override string DanhGiaMucDoBanBuon()
        {
            if (SoLuongTon < 3)
                return "Bán được";
            return "Không đánh giá";
        }

        public override string ToString()
        {
            return base.ToString() + $" | BH: {ThoiGianBaoHanh} tháng, CS: {CongSuat} KW";
        }
    }

    // 4. LỚP HÀNG SÀNH SỨ
    public class HangSanhSu : HangHoa
    {
        public string NhaSanXuat { get; set; }
        public DateTime NgayNhapKho { get; set; }

        public HangSanhSu(string maHang, string tenHang, decimal donGia, int soLuongTon, string nhaSanXuat, DateTime ngayNhapKho)
            : base(maHang, tenHang, donGia, soLuongTon)
        {
            NhaSanXuat = nhaSanXuat;
            NgayNhapKho = ngayNhapKho;
        }

        public override decimal TinhTienVAT()
        {
            return DonGia * 0.10m; // VAT 10%
        }

        public override string DanhGiaMucDoBanBuon()
        {
            // Tồn kho > 50 và thời gian lưu kho > 10 ngày
            if (SoLuongTon > 50 && (DateTime.Now - NgayNhapKho).TotalDays > 10)
                return "Bán chậm";
            return "Không đánh giá";
        }

        public override string ToString()
        {
            return base.ToString() + $" | NSX: {NhaSanXuat} (Nhập: {NgayNhapKho:dd/MM/yyyy})";
        }
    }

    // 5. LỚP QUẢN LÝ DANH SÁCH HÀNG HÓA
    public class DanhSachHangHoa
    {
        private List<HangHoa> danhSach;

        public DanhSachHangHoa()
        {
            danhSach = new List<HangHoa>();
        }

        public bool ThemHangHoa(HangHoa hh)
        {
            // Kiểm tra trùng mã hàng
            foreach (var item in danhSach)
            {
                if (item.MaHang.Equals(hh.MaHang, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Lỗi: Mã hàng '{hh.MaHang}' đã tồn tại!");
                    return false;
                }
            }
            danhSach.Add(hh);
            return true;
        }

        public void XuatDanhSach()
        {
            Console.WriteLine(new string('=', 125));
            Console.WriteLine($"{"Mã Hàng",-10} | {"Tên Hàng",-20} | {"Đơn Giá",12} | {"SL Tồn",10} | {"Tiền VAT",12} | {"Đánh Giá",-15} | Thông tin thêm");
            Console.WriteLine(new string('-', 125));

            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống!");
            }
            else
            {
                foreach (var hh in danhSach)
                {
                    Console.WriteLine(hh.ToString());
                }
            }
            Console.WriteLine(new string('=', 125));
        }
    }

    // 6. CHƯƠNG TRÌNH CHÍNH
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DanhSachHangHoa ds = new DanhSachHangHoa();

            // Nhập cứng một số dữ liệu mẫu để Test
            Console.WriteLine("Đang khởi tạo danh sách hàng hóa...\n");

            // 1. Thêm hàng thực phẩm
            HangThucPham tp1 = new HangThucPham("TP01", "Gạo ST25", 25000, 10, "VinMart", new DateTime(2023, 1, 1), new DateTime(2023, 12, 31)); // Đã hết hạn -> Khó bán
            HangThucPham tp2 = new HangThucPham("TP02", "Sữa tươi", 35000, 50, "TH True Milk", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(20));
            ds.ThemHangHoa(tp1);
            ds.ThemHangHoa(tp2);

            // 2. Thêm hàng điện máy
            HangDienMay dm1 = new HangDienMay("DM01", "Tủ lạnh Samsung", 15000000, 2, 24, 1.5m); // Tồn < 3 -> Bán được
            HangDienMay dm2 = new HangDienMay("DM02", "Tivi Sony", 20000000, 15, 36, 2.0m);
            ds.ThemHangHoa(dm1);
            ds.ThemHangHoa(dm2);

            // 3. Thêm hàng sành sứ
            HangSanhSu ss1 = new HangSanhSu("SS01", "Chén sứ Hải Dương", 50000, 100, "Minh Long", DateTime.Now.AddDays(-15)); // Tồn > 50, > 10 ngày -> Bán chậm
            HangSanhSu ss2 = new HangSanhSu("SS02", "Bình hoa gốm", 250000, 10, "Bát Tràng", DateTime.Now.AddDays(-2));
            ds.ThemHangHoa(ss1);
            ds.ThemHangHoa(ss2);

            // In danh sách ra màn hình
            ds.XuatDanhSach();

            Console.WriteLine("\nBấm phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}