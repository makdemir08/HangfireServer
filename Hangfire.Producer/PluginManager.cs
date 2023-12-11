using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ERPPlugin;
using ERPPlugin.Dto;

public class PluginManager
{
    private readonly List<dynamic> pluginInstances;

    public PluginManager()
    {
        pluginInstances = new List<dynamic>();
        LoadPlugins();
    }

    private void LoadPlugins()
    {
        foreach (var item in Directory.GetFiles("Plugin", "*.dll"))
        {
            Assembly assembly = Assembly.LoadFrom(item);
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetInterface("IERPPlugin") != null)
                {
                    dynamic instance = Activator.CreateInstance(type);
                    pluginInstances.Add(instance);
                }
            }
        }
    }

    public async Task<List<Title>> GetTitlesFromPlugins()
    {
        List<Title> titleList = new List<Title>();

        foreach (var instance in pluginInstances)
        {
            titleList.AddRange(await instance.GetTitle());
        }

        return titleList;
    }

    public async Task<List<User>> GetUsersFromPlugins()
    {
        List<User> userList = new List<User>();

        foreach (var instance in pluginInstances)
        {
            userList.AddRange(await instance.GetTitle());
        }

        return userList;
    }
}
