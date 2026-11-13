using System.Collections.Generic;
using System.Data;
using BT6._8.DTO;

namespace BT6._8.DAL
{
    public class BanBidaDAL
    {
        public List<BanBidaDTO> LayDanhSach()
        {
            List<BanBidaDTO> ds = new List<BanBidaDTO>();
            foreach (DataRow r in DatabaseHelper.ExecuteQuery("SELECT MaBan, TenBan, LoaiBan, GiaTheoGio, TrangThai FROM BanBida ORDER BY MaBan").Rows)
            {
                ds.Add(new BanBidaDTO
                {
                    MaBan = (int)r["MaBan"],
                    TenBan = r["TenBan"].ToString(),
                    LoaiBan = r["LoaiBan"].ToString(),
                    GiaTheoGio = (decimal)r["GiaTheoGio"],
                    TrangThai = r["TrangThai"].ToString()
                });
            }
            return ds;
        }
    }
}
