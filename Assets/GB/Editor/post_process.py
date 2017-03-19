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

facebookAppID = "379212652436292"

sns_setting = [{
	"CFBundleTypeRole" : "Editor",
	"CFBundleURLName" : "%s" % (bundle_identifier),
	"CFBundleURLSchemes" : ["fb%s" % facebookAppID]
}]

plist["CFBundleURLTypes"] = sns_setting

'''
<key>LSApplicationQueriesSchemes</key>
<array>
    <string>fbapi</string>
    <string>fbapi20130214</string>
    <string>fbapi20130410</string>
    <string>fbapi20130702</string>
    <string>fbapi20131010</string>
    <string>fbapi20131219</string>    
    <string>fbapi20140410</string>
    <string>fbapi20140116</string>
    <string>fbapi20150313</string>
    <string>fbapi20150629</string>
    <string>fbapi20160328</string> 
    <string>fbauth</string>
    <string>fbauth2</string>
    <string>fb-messenger-api20140430</string>
</array>	
'''
fb_schmes = [
	"fbauth", "fbauth2", "fbapi",
]

plist["LSApplicationQueriesSchemes"] = fb_schmes

if len(facebookAppID) > 0:
    plist["FacebookAppID"] = facebookAppID

plist["AppLovinSdkKey"] = "wsGT89gFuGFIZrLsp6MrS_TQaRU_HuBCkSftbL6UcMnAB61_DOqgOI5zkaz0S9CAbt2CC8gqUS_gZ0fnPURonX"
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
project.add_file(frameworkPath + 'Bolts.framework', tree='SDKROOT')
project.add_file(frameworkPath + 'FBSDKCoreKit.framework', tree='SDKROOT')
project.add_file(frameworkPath + 'FBSDKLoginKit.framework', tree='SDKROOT')
project.add_file(frameworkPath + 'VungleSDK.framework', tree='SDKROOT')
project.add_file('System/Library/Frameworks/AdSupport.framework', tree='SDKROOT')
project.add_framework_search_paths(frameworkPath)

log('------------------------------------------------------------\n')
log('			2-1. iOS9 Delete / Changed Library path          \n')
log('------------------------------------------------------------\n')
project.add_file('usr/lib/libz.tbd', tree='SDKROOT')
project.add_file('usr/lib/libsqlite3.tbd', tree='SDKROOT')


log('------------------------------------------------------------\n')
log('			3. Set Flag in Project Build Setting             \n')
log('------------------------------------------------------------\n')
project.add_other_ldflags('-ObjC')
project.add_single_valued_flag('ENABLE_BITCODE', 'NO')
project.add_single_valued_flag('CLANG_ENABLE_MODULES', 'YES')
project.save()
log('------------------------------\n'
	'      	  Saved Project.       \n'
	'------------------------------')
