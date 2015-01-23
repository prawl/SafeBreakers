# SafeBreakers

Make sure Unity3d is properly configured by enabling these settings found here. 
http://unity3diy.blogspot.com/2014/06/using-git-with-3d-games-source-control_8.html

# Tips

- Simply opening the project and playing the secene will cause git to pickup changes dispite the fact that no changes were made.  Reseting using 'git reset --hard master' will remove the changes

# Creating Pull Requests
- Create a new branch with "git branch BRANCH_NAME"
- Switch over to the new branch with "git checkout BRANCH_NAME"  *Note you need a clean working directory to do this
- Push to github with "git push origin BRANCH_NAME"
- If you just pushed your branch github will show a button that says "create pull request" if not click "pull requests" on the right side of the page and create a new pull request with the pushed branch
