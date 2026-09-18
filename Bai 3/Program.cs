using System;
using System.Collections.Generic;
using System.Globalization;

namespace QuanLyGiaoDich
{
    // 1. LỚP CƠ SỞ GIAO DỊCH
    public class GiaoDich
    {
        public string MaGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }

        public GiaoDich() { }

        public GiaoDich(string maGiaoDich, DateTime ngayGiaoDich, decimal donGia, int soLuong)
        {
            MaGiaoDich = maGiaoDich;
            NgayGiaoDich = ngayGiaoDich;
            DonGia = donGia;
            SoLuong = soLuong;
        }

        public virtual void Nhap()
        {
            Console.Write("  + Mã giao dịch: ");
            MaGiaoDich = Console.ReadLine();

            Console.Write("  + Ngày giao dịch (dd/MM/yyyy): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                NgayGiaoDich = dt;
            }
            else
            {
                NgayGiaoDich = DateTime.Now;
            }

            Console.Write("  + Đơn giá: ");
            DonGia = decimal.TryParse(Console.ReadLine(), out decimal dg) ? dg : 0;

            Console.Write("  + Số lượng: ");
            SoLuong = int.TryParse(Console.ReadLine(), out int sl) ? sl : 0;
        }

        public virtual decimal TinhThanhTien()
        {
            return SoLuong * DonGia;
        }

        public virtual void Xuat()
        {
            Console.Write($"[Mã: {MaGiaoDich,-6} | Ngày: {NgayGiaoDich:dd/MM/yyyy} | Đơn giá: {DonGia:N0} | Số lượng: {SoLuong}");
        }
    }

    // 2. LỚP GIAO DỊCH VÀNG
    public class GiaoDichVang : GiaoDich
    {
        public string LoaiVang { get; set; }

        public GiaoDichVang() { }

        public GiaoDichVang(string maGiaoDich, DateTime ngayGiaoDich, decimal donGia, int soLuong, string loaiVang)
            : base(maGiaoDich, ngayGiaoDich, donGia, soLuong)
        {
            LoaiVang = loaiVang;
        }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("  + Loại vàng (9999 / 24K / 18K...): ");
            LoaiVang = Console.ReadLine();
        }

        public override decimal TinhThanhTien()
        {
            return SoLuong * DonGia;
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine($" | Loại vàng: {LoaiVang,-5} | Thành tiền: {TinhThanhTien():N0} VNĐ]");
        }
    }

    // 3. LỚP GIAO DỊCH TIỀN TỆ
    public class GiaoDichTienTe : GiaoDich
    {
        public decimal TyGia { get; set; }
        public string LoaiTienTe { get; set; } // VND, USD, EUR

        public GiaoDichTienTe() { }

        public GiaoDichTienTe(string maGiaoDich, DateTime ngayGiaoDich, decimal donGia, int soLuong, decimal tyGia, string loaiTienTe)
            : base(maGiaoDich, ngayGiaoDich, donGia, soLuong)
        {
            TyGia = tyGia;
            LoaiTienTe = loaiTienTe;
        }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("  + Chọn loại tiền (1 - VND, 2 - USD, 3 - EUR): ");
            string luaChon = Console.ReadLine()?.Trim();

            switch (luaChon)
            {
                case "2":
                    LoaiTienTe = "USD";
                    Console.Write("  + Nhập tỷ giá USD: ");
                    TyGia = decimal.TryParse(Console.ReadLine(), out decimal tgUSD) ? tgUSD : 1;
                    break;
                case "3":
                    LoaiTienTe = "EUR";
                    Console.Write("  + Nhập tỷ giá EUR: ");
                    TyGia = decimal.TryParse(Console.ReadLine(), out decimal tgEUR) ? tgEUR : 1;
                    break;
                default:
                    LoaiTienTe = "VND";
                    TyGia = 1;
                    break;
            }
        }

        public override decimal TinhThanhTien()
        {
            if (LoaiTienTe.Equals("VND", StringComparison.OrdinalIgnoreCase))
            {
                return SoLuong * DonGia;
            }
            return SoLuong * DonGia * TyGia;
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine($" | Loai tiền: {LoaiTienTe,-4} | Tỷ giá: {TyGia:N0} | Thành tiền: {TinhThanhTien():N0} VNĐ]");
        }
    }

    // 4. CHƯƠNG TRÌNH CHÍNH
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            List<GiaoDichVang> dsVang = new List<GiaoDichVang>();
            List<GiaoDichTienTe> dsTienTe = new List<GiaoDichTienTe>();

            Console.WriteLine("================ NHẬP GIAO DỊCH VÀNG ================");
            Console.Write("Nhập số lượng giao dịch vàng: ");
            int nVang = int.TryParse(Console.ReadLine(), out int nv) ? nv : 0;
            for (int i = 0; i < nVang; i++)
            {
                Console.WriteLine($"\n--- Giao dịch vàng #{i + 1} ---");
                GiaoDichVang gdVang = new GiaoDichVang();
                gdVang.Nhap();
                dsVang.Add(gdVang);
            }

            Console.WriteLine("\n================ NHẬP GIAO DỊCH TIỀN TỆ ================");
            Console.Write("Nhập số lượng giao dịch tiền tệ: ");
            int nTienTe = int.TryParse(Console.ReadLine(), out int nt) ? nt : 0;
            for (int i = 0; i < nTienTe; i++)
            {
                Console.WriteLine($"\n--- Giao dịch tiền tệ #{i + 1} ---");
                GiaoDichTienTe gdTienTe = new GiaoDichTienTe();
                gdTienTe.Nhap();
                dsTienTe.Add(gdTienTe);
            }

            // Hiển thị danh sách
            Console.WriteLine("\n================ DANH SÁCH GIAO DỊCH VÀNG ================");
            int tongSoLuongVang = 0;
            foreach (var gd in dsVang)
            {
                gd.Xuat();
                tongSoLuongVang += gd.SoLuong;
            }

            Console.WriteLine("\n================ DANH SÁCH GIAO DỊCH TIỀN TỆ ================");
            int tongSoLuongTienTe = 0;
            decimal tongThanhTienTienTe = 0;
            foreach (var gd in dsTienTe)
            {
                gd.Xuat();
                tongSoLuongTienTe += gd.SoLuong;
                tongThanhTienTienTe += gd.TinhThanhTien();
            }

            // Kết quả thống kê
            Console.WriteLine("\n================ KẾT QUẢ THỐNG KÊ ================");
            Console.WriteLine($"1. Tổng số lượng giao dịch Vàng: {tongSoLuongVang:N0}");
            Console.WriteLine($"2. Tổng số lượng giao dịch Tiền tệ: {tongSoLuongTienTe:N0}");

            if (dsTienTe.Count > 0)
            {
                decimal tbThanhTienTienTe = tongThanhTienTienTe / dsTienTe.Count;
                Console.WriteLine($"3. Trung bình cộng thành tiền của Giao dịch Tiền tệ: {tbThanhTienTienTe:N0} VNĐ");
            }
            else
            {
                Console.WriteLine("3. Không có giao dịch tiền tệ nào để tính trung bình cộng.");
            }

            // Lọc giao dịch đơn giá > 1 tỷ
            Console.WriteLine("\n================ GIAO DỊCH CÓ ĐƠN GIÁ > 1 TỶ ================");
            bool coGiaoDichLon = false;
            const decimal MOT_TY = 1_000_000_000m;

            foreach (var gd in dsVang)
            {
                if (gd.DonGia > MOT_TY)
                {
                    gd.Xuat();
                    coGiaoDichLon = true;
                }
            }
            foreach (var gd in dsTienTe)
            {
                if (gd.DonGia > MOT_TY)
                {
                    gd.Xuat();
                    coGiaoDichLon = true;
                }
            }

            if (!coGiaoDichLon)
            {
                Console.WriteLine("Không có giao dịch nào có đơn giá lớn hơn 1,000,000,000 VNĐ.");
            }

            Console.WriteLine("\nBấm phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}