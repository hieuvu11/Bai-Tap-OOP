using System;
using System.Collections.Generic;

namespace QuanLySach
{
    // 1. LỚP CƠ SỞ: SÁCH
    public class Sach
    {
        public string MaSach { get; set; }
        public DateTime NgayNhap { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public string NhaXuatBan { get; set; }

        public Sach() { }

        public Sach(string maSach, DateTime ngayNhap, double donGia, int soLuong, string nhaXuatBan)
        {
            MaSach = maSach;
            NgayNhap = ngayNhap;
            DonGia = donGia;
            SoLuong = soLuong;
            NhaXuatBan = nhaXuatBan;
        }

        public virtual void Nhap()
        {
            Console.Write("  - Nhập mã sách: ");
            MaSach = Console.ReadLine();
            Console.Write("  - Nhập ngày nhập (dd/MM/yyyy): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
            {
                NgayNhap = dt;
            }
            else
            {
                NgayNhap = DateTime.Now;
            }
            Console.Write("  - Nhập đơn giá: ");
            DonGia = double.Parse(Console.ReadLine());
            Console.Write("  - Nhập số lượng: ");
            SoLuong = int.Parse(Console.ReadLine());
            Console.Write("  - Nhập nhà xuất bản: ");
            NhaXuatBan = Console.ReadLine();
        }

        public virtual void Xuat()
        {
            Console.Write($"Mã: {MaSach} | Ngày nhập: {NgayNhap:dd/MM/yyyy} | Đơn giá: {DonGia:N0} | Số lượng: {SoLuong} | NXB: {NhaXuatBan}");
        }

        public virtual double TinhThanhTien()
        {
            return SoLuong * DonGia;
        }
    }

    // 2. LỚP SÁCH GIÁO KHOA
    public class SachGiaoKhoa : Sach
    {
        // TinhTrang: true = Mới, false = Cũ
        public bool TinhTrang { get; set; }

        public SachGiaoKhoa() { }

        public SachGiaoKhoa(string maSach, DateTime ngayNhap, double donGia, int soLuong, string nhaXuatBan, bool tinhTrang)
            : base(maSach, ngayNhap, donGia, soLuong, nhaXuatBan)
        {
            TinhTrang = tinhTrang;
        }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("  - Tình trạng sách (1 - Mới, 0 - Cũ): ");
            int luaChon = int.Parse(Console.ReadLine());
            TinhTrang = (luaChon == 1);
        }

        public override double TinhThanhTien()
        {
            if (TinhTrang)
            {
                return SoLuong * DonGia;
            }
            else
            {
                return SoLuong * DonGia * 0.5;
            }
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine($" | Tình trạng: {(TinhTrang ? "Mới" : "Cũ")} | Thành tiền: {TinhThanhTien():N0} VNĐ");
        }
    }

    // 3. LỚP SÁCH THAM KHẢO
    public class SachThamKhao : Sach
    {
        public double Thue { get; set; }

        public SachThamKhao() { }

        public SachThamKhao(string maSach, DateTime ngayNhap, double donGia, int soLuong, string nhaXuatBan, double thue)
            : base(maSach, ngayNhap, donGia, soLuong, nhaXuatBan)
        {
            Thue = thue;
        }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("  - Nhập thuế: ");
            Thue = double.Parse(Console.ReadLine());
        }

        public override double TinhThanhTien()
        {
            return SoLuong * DonGia + Thue;
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine($" | Thuế: {Thue:N0} | Thành tiền: {TinhThanhTien():N0} VNĐ");
        }
    }

    // 4. CHƯƠNG TRÌNH CHÍNH
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            List<SachGiaoKhoa> dsSGK = new List<SachGiaoKhoa>();
            List<SachThamKhao> dsSTK = new List<SachThamKhao>();

            Console.Write("Nhập số lượng sách giáo khoa: ");
            int nSGK = int.Parse(Console.ReadLine());
            for (int i = 0; i < nSGK; i++)
            {
                Console.WriteLine($"\n--- Nhập Sách Giáo Khoa thứ {i + 1} ---");
                SachGiaoKhoa sgk = new SachGiaoKhoa();
                sgk.Nhap();
                dsSGK.Add(sgk);
            }

            Console.Write("\nNhập số lượng sách tham khảo: ");
            int nSTK = int.Parse(Console.ReadLine());
            for (int i = 0; i < nSTK; i++)
            {
                Console.WriteLine($"\n--- Nhập Sách Tham Khảo thứ {i + 1} ---");
                SachThamKhao stk = new SachThamKhao();
                stk.Nhap();
                dsSTK.Add(stk);
            }

            // Xuất danh sách & tính toán
            Console.WriteLine("\n================ DANH SÁCH SÁCH GIÁO KHOA ================");
            double tongTienSGK = 0;
            foreach (var item in dsSGK)
            {
                item.Xuat();
                tongTienSGK += item.TinhThanhTien();
            }

            Console.WriteLine("\n================ DANH SÁCH SÁCH THAM KHẢO ================");
            double tongTienSTK = 0;
            double tongDonGiaSTK = 0;
            foreach (var item in dsSTK)
            {
                item.Xuat();
                tongTienSTK += item.TinhThanhTien();
                tongDonGiaSTK += item.DonGia;
            }

            Console.WriteLine("\n================ KẾT QUẢ THỐNG KÊ ================");
            Console.WriteLine($"1. Tổng thành tiền Sách Giáo Khoa: {tongTienSGK:N0} VNĐ");
            Console.WriteLine($"2. Tổng thành tiền Sách Tham Khảo: {tongTienSTK:N0} VNĐ");

            if (dsSTK.Count > 0)
            {
                double trungBinhDonGiaSTK = tongDonGiaSTK / dsSTK.Count;
                Console.WriteLine($"3. Trung bình cộng đơn giá Sách Tham Khảo: {trungBinhDonGiaSTK:N0} VNĐ");
            }
            else
            {
                Console.WriteLine("3. Không có sách tham khảo để tính trung bình cộng đơn giá.");
            }

            Console.Write("\n4. Nhập tên Nhà xuất bản cần tìm cho Sách Giáo Khoa: ");
            string nxbTimKiem = Console.ReadLine();
            Console.WriteLine($"--- Các Sách Giáo Khoa thuộc NXB '{nxbTimKiem}': ---");
            bool timThay = false;
            foreach (var sgk in dsSGK)
            {
                if (sgk.NhaXuatBan.Equals(nxbTimKiem, StringComparison.OrdinalIgnoreCase))
                {
                    sgk.Xuat();
                    timThay = true;
                }
            }
            if (!timThay)
            {
                Console.WriteLine($"Không tìm thấy sách giáo khoa nào của NXB '{nxbTimKiem}'.");
            }

            Console.WriteLine("\nBấm phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}