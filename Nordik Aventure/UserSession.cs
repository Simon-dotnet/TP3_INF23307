public class UserSession
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserSession(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // Garde en mémoire l'id du user connecté
    public int? UserId
    {
        get
        {
            var idStr = _httpContextAccessor.HttpContext?.Session.GetString("userId");
            return idStr != null ? int.Parse(idStr) : null;
        }
        set
        {
            if (value.HasValue)
                _httpContextAccessor.HttpContext?.Session.SetString("userId", value.Value.ToString());
        }
    }

    // Vide la session
    public void ClearSession()
    {
        _httpContextAccessor.HttpContext?.Session.Clear();
    }
}