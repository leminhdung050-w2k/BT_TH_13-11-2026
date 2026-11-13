using System.Collections.Generic;
using BT6._8.DAL;
using BT6._8.DTO;

namespace BT6._8.BUS
{
    public class BanBidaBUS
    {
        private readonly BanBidaDAL dal = new BanBidaDAL();

        public List<BanBidaDTO> LayDanhSach()
        {
            return dal.LayDanhSach();
        }
    }
}
