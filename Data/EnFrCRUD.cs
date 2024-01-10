using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data;


public class EnFrCRUD //Entity Framework CRUD
{
    private readonly ApplicationDbContext _appDb;

    public EnFrCRUD(ApplicationDbContext appDb)
    {
        _appDb = appDb;
    }

    public List<Notebook> GetAll()
    {
        return _appDb.Notebook.ToList();
    }
    public Notebook GetByID(int ID)
    {
        return _appDb.Notebook.FirstOrDefault(x => x.Id == ID);
    }
    public void CreateNote(Notebook notebook)
    {
        _appDb.Notebook.Add(notebook);
        _appDb.SaveChanges();

    }
    public void UpdateNotebook(Notebook notebook)
    {
        //_appDb.Notebooks.Update(notebook);
        _appDb.Entry(notebook).State = EntityState.Modified;
        _appDb.SaveChanges();
    }
    public bool DeleteNotes(int ID)
    {
        
        var findID = _appDb.Notebook.Find(ID);
        if (findID != null)
        {
            _appDb.Notebook.Remove(findID);
            _appDb.SaveChanges();
            return true;
        }
        return false;
    }
}
