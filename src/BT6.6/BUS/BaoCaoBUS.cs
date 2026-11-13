using System;
using System.Collections.Generic;
using BT6._6.DAL;
using BT6._6.DTO;

namespace BT6._6.BUS
{
    public class BaoCaoBUS
    {
        private readonly BaoCaoDAL dal = new BaoCaoDAL();

        public List<PhieuMuonDTO> PhieuQuaHan()
        {
            return dal.PhieuQuaHan();
        }

        public int DemQuaHan()
        {
            return dal.DemQuaHan();
        }

        public int TongLuotMuon()
        {
            return dal.TongLuotMuon();
        }

        // Tỷ lệ % lượt mượn của mỗi thể loại so với tổng
        public List<ThongKeDTO> ThongKeTheLoai()
        {
            List<ThongKeDTO> ds = dal.ThongKeTheLoai();
            int tong = 0;
            foreach (ThongKeDTO t in ds) tong += t.SoLuotMuon;
            foreach (ThongKeDTO t in ds) t.TyLe = tong == 0 ? 0 : Math.Round(100.0 * t.SoLuotMuon / tong, 1);
            return ds;
        }
    }
}
