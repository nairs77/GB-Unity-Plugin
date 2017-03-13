#!/usr/bin/env python
# Open Source mod_pbxproj.py
# https://github.com/kronenthaler/mod-pbxproj
# mode_pbxproj.py mod_pbxproj3.py

# This module can read, modify, and write a .pbxproj file from an Xcode project. 
# The file is usually called project.pbxproj and can be found inside the .xcodeproj bundle.

# Last written by nairs77 2014. 12. 04

# *********** SCRIPT CUSTOMIZATION ***********

import os, shutil
import plistlib
import errno
from sys import argv
from mod_pbxproj import XcodeProject

projectPath = argv[1]
frameworkPath = argv[2]

def log(x):
	with open('GBiOSBuildProcessLog.txt', 'a') as f:
		f.write(x + "\n")

log('------------------------------------------------------------\n')
log('      				Start post_process.py                    \n')
log('------------------------------------------------------------\n')

log('Unity Project Path --> ' + projectPath)
log('FrameWork Path --> ' + frameworkPath)

log('------------------------------------------------------------\n')
log('                  1. Register SNS info                      \n')
log('------------------------------------------------------------\n')

plist_path = os.path.join(projectPath, 'Info.plist')
plist = plistlib.readPlist(plist_path)

# usage
# 1. Facebook login
#  - Add CFBundleURLSchemes / fb{FACEBOOK_APP_ID}
#  - Add FacebookAppID 
#  - Add FacebookDisplayName 
# 2. Google Plus
#  - Add CFBundleURLSchemes / ${Bundle identifier}
# 3. Twitter
#  - Add CFBundleURLSchemes / tw.{$Bundle identifier}

bundle_identifier = plist["CFBundleIdentifier"]

facebookAppID = "539935412817527"

sns_setting = [{
	"CFBundleTypeRole" : "Editor",
	"CFBundleURLName" : "%s" % (bundle_identifier),
	"CFBundleURLSchemes" : ["fb%s" % facebookAppID]
}]

plist["CFBundleURLTypes"] = sns_setting

if len(facebookAppID) > 0:
    plist["FacebookAppID"] = facebookAppID

plistlib.writePlist(plist, plist_path)

log('------------------------------------------------------------\n')
log('			2. Add library (Framework) in Project            \n')
log('------------------------------------------------------------\n')

project = XcodeProject.Load(projectPath + '/Unity-iPhone.xcodeproj/project.pbxproj')
log('Loaded project.pbxproj.')	


result = project.add_file(frameworkPath + 'GBSdk.framework', tree='SDKROOT')	
log('Added GBSdk SDK Framework')

project.add_file(frameworkPath + 'GoogleMobileAds.framework', tree='SDKROOT')
project.add_file(frameworkPath + 'UnityAds.framework', tree='SDKROOT')
project.add_file(frameworkPath + 'AppLovinSDK.framework', tree='SDKROOT')

project.add_framework_search_paths(frameworkPath)

log('------------------------------------------------------------\n')
log('			2-1. iOS9 Delete / Changed Library path          \n')
log('------------------------------------------------------------\n')
# project.add_file('usr/lib/libiconv.2.tbd', true='SDKROOT')
# project.remove_file_by_path('usr/lib/libiconv.2.dylib')


log('------------------------------------------------------------\n')
log('			3. Set Flag in Project Build Setting             \n')
log('------------------------------------------------------------\n')
project.add_other_ldflags('-ObjC')
project.add_single_valued_flag('ENABLE_BITCODE', 'NO')
project.saveFormat3_2()
log('------------------------------\n'
	'      	  Saved Project.       \n'
	'------------------------------')
