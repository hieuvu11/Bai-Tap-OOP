using System;
using System.Collections.Generic;
using System.Text;

namespace bai1
{
    // ===== LỚP CHA =====
    public class ChuyenXe
    {
        protected string maChuyen;
        protected string tenTaiXe;
        protected string bienSoXe;
        protected double doanhThu;

        public double DoanhThu
        {
            get { return doanhThu; }
            set { doanhThu = value; }
        }

        public ChuyenXe(string maChuyen, string tenTaiXe, string bienSoXe, double doanhThu)
        {
            this.maChuyen = maChuyen;
            this.tenTaiXe = tenTaiXe;
            this.bienSoXe = bienSoXe;
            this.doanhThu = doanhThu;
        }

        public virtual void XuatThongTin()
        {
            Console.WriteLine($"Chuyến: {maChuyen} | Tài xế: {tenTaiXe} | Biển số: {bienSoXe} | Doanh thu: {doanhThu:N0} VNĐ");
        }
    }

    // ===== LỚP CON: CHUYẾN XE NỘI THÀNH =====
    internal class ChuyenXeNoiThanh : ChuyenXe
    {
        private int tuyenSo;
        private double quangDuong;

        public ChuyenXeNoiThanh(string maChuyen, string tenTaiXe, string bienSoXe, double doanhThu, int tuyenSo, double quangDuong)
            : base(maChuyen, tenTaiXe, bienSoXe, doanhThu)
        {
            this.tuyenSo = tuyenSo;
            this.quangDuong = quangDuong;
        }

        public override void XuatThongTin()
        {
            base.XuatThongTin();
            Console.WriteLine($"   -> [Nội thành] Tuyến: {tuyenSo} | Quãng đường: {quangDuong} km\n");
        }
    }

    // ===== LỚP CON: CHUYẾN XE NGOẠI THÀNH =====
    internal class ChuyenXeNgoaiThanh : ChuyenXe
    {
        private string diemDen;
        private int soNgay;

        public ChuyenXeNgoaiThanh(string maChuyen, string tenTaiXe, string bienSoXe, double doanhThu, string diemDen, int soNgay)
            : base(maChuyen, tenTaiXe, bienSoXe, doanhThu)
        {
            this.diemDen = diemDen;
            this.soNgay = soNgay;
        }

        public override void XuatThongTin()
        {
            base.XuatThongTin();
            Console.WriteLine($"   -> [Ngoại thành] Điểm đến: {diemDen} | Thời gian: {soNgay} ngày\n");
        }
    }

    // ===== CHƯƠNG TRÌNH CHÍNH =====
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<ChuyenXe> dsChuyen = new List<ChuyenXe>();

            // Thêm chuyến xe nội thành
            dsChuyen.Add(new ChuyenXeNoiThanh("NT101", "Đặng Minh Tuấn", "29A-11223", 450000, 3, 12));
            dsChuyen.Add(new ChuyenXeNoiThanh("NT102", "Võ Thị Hoa", "29B-33445", 520000, 5, 18.5));

            // Thêm chuyến xe ngoại thành
            dsChuyen.Add(new ChuyenXeNgoaiThanh("NG101", "Bùi Quốc Hùng", "30A-55667", 1200000, "Hà Nội - Sa Pa", 2));
            dsChuyen.Add(new ChuyenXeNgoaiThanh("NG102", "Lý Thanh Sơn", "30B-77889", 1800000, "Hà Nội - Ninh Bình", 1));

            // In danh sách chuyến xe
            Console.WriteLine("=== DANH SÁCH CHUYẾN XE ===");
            foreach (var chuyen in dsChuyen)
            {
                chuyen.XuatThongTin();
            }

            // Tính tổng doanh thu
            double tongThu = 0;
            foreach (var chuyen in dsChuyen)
            {
                tongThu += chuyen.DoanhThu;
            }
            Console.WriteLine($"Số chuyến: {dsChuyen.Count}");
            Console.WriteLine($"Tổng doanh thu: {tongThu:N0} VNĐ");
        }
    }
}