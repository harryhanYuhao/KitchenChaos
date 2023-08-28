// static data are not destoryed when scene is changed
// they must be destoryed manually
// this script is attached to a game object on main menu scene

public static class ResetStaticDataManager 
{
    public static void ResetStaticData()
    {
        CuttingCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
        Loader.ResetStaticData();
        PlateObject.ResetStaticData();
        Player.ResetStaticData();
    }
}
