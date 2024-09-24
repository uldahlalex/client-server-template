using DataAccess.TransferModels.Request;
using Service.TransferModels.Responses;

namespace Service.Interfaces;

public interface IPaperService
{
    public PaperDto CreatePaper(CreatePaperDto createPaperDto);
}