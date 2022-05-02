using BookService.Dtos;
using BookService.Entities;

namespace BookService.AppServices
{
    public interface IBookEntityFactory
    {
        BookEntity Create();
    }
}