Copy-Item "bin/Debug/$Env:modname-Merged.dll" -Destination "$Env:onisavefolder\mods\dev\$Env:modname" -force
Copy-Item "mod.yaml" -Destination "$Env:onisavefolder\mods\dev\$Env:modname" -force
Copy-Item "mod_info.yaml" -Destination "$Env:onisavefolder\mods\dev\$Env:modname" -force

# Path should point to the steam url shortcut (the same one created by steam in your desktop)
invoke-item "$Env:oniurlpath"

Get-Content "$Env:onilogfolder\Player.log" -Wait