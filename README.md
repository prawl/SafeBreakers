# SafeBreakers

Make sure Unity3d is properly configured by enabling these settings found here. 
http://unity3diy.blogspot.com/2014/06/using-git-with-3d-games-source-control_8.html

# Tips

- Simply opening the project and playing the secene will cause git to pickup changes dispite the fact that no changes were made.  Reseting using 'git reset --hard master' will remove the changes

# Rebasing A Branch

- Pull down the latest changes while on the master branch "get pull origin master"
- Switch over to your branch "git checkout [branch_name]
- Bring in the newest changes from master to your branch with "get rebase master" 
- If there are merge conflicts that means we changed the same file, you need to open that file and remove the changes you dont want, then type "git add --all" then "git rebase --continue" 
- Push changes to github "git push origin [branch_name]
- Create a new pull request 

# Switching Branches
- Use "git checkout [branch_name]"
- You need to have a clean directory before switching branches *AKA no red files lines, Hide your changes with "git stash" 
- Move to desired branch and get your changes back with "git stash pop"

# Creating Pull Requests

- If you just pushed up your branch you should see a button pop up that says something like "Create new pull request [branch_name] 
- Create the pull request then click the button at the bottom that says "Merge" *This will put your branch on top of master
- If no buttons appears there should be a "Create pull request" button on the upper right hand corner of this page.
