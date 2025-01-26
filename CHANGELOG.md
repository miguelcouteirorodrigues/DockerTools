# Change Log

All notable changes to this project will be documented in this file. See [versionize](https://github.com/versionize/versionize) for commit guidelines.


<a name="1.0.0"></a>
## [1.0.0](https://www.github.com/miguelcouteirorodrigues/DockerTools/releases/tag/v1.0.0) (2025-01-26)

### Features

* Add connection test to client initialization ([fdd7ef9](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/fdd7ef9b2a0938bd56e14a0ac58c3c6e2f88b571))
* Add logs from commands to console ([94a4fb6](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/94a4fb64a07991981c23d01625d3a3dcbe6219a5))
* Add support for running commands on a container ([0717a60](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/0717a6087beaa3dbb63d3bbe5c0a58423fa9ace5))
* Allow auto detection of Docker Remote API ([6c91814](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/6c9181403dde2a62fb43134d8f423674fc0593cd))
* Allow for manual setup of the container version for postgres and postgis containers ([ef4e70e](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/ef4e70e42b6a996f9ddeecead444f17066d10847))
* Allow UsingEnvironmentDetection to detect HTTP API availability ([3d226f7](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/3d226f7fc8ba3fa21d88a30f07c3c2af8dcc4a40))
* Alter how instances are initialized ([51d802f](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/51d802fc74958dcf73c0cdeab49c12981dfdff71))
* Improve command execution feedback ([6616757](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/66167572315b1d0107ac31b2656f598a5b423568))
* Initial commit ([7170057](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/717005767f77cedac6c6ff11d4268bc8736f18b6))
* publish on nuget.org ([#20](https://www.github.com/miguelcouteirorodrigues/DockerTools/issues/20)) ([139337d](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/139337dc2125b2791cfaef27dcf81839eb3e80ad))
* Try versionize ([11b2dfa](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/11b2dfa48c627e4602fe2b9913c3e61a1cceb6ab))
* Versionize support ([#22](https://www.github.com/miguelcouteirorodrigues/DockerTools/issues/22)) ([46dd583](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/46dd583f4dc70354ffcb26c9105e5ea882fed2a9))

### Bug Fixes

* Add timeout to client ping on creation ([f3ca755](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/f3ca755be5189f35782a9f61609871de78bca941))
* Await container command execution ([2ffa912](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/2ffa912f1741fa970b4b64ab47529441cb77b7bc))
* Config internal postgres environment variables to avoid root user warning message ([56d7bfb](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/56d7bfbee9e40122f0d8bab37c75e8333bc419e3))
* Correct connection verification for Unix environments ([0b90bfe](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/0b90bfe21c890f129b86d1ac4cb02219937a3c78))
* Correct nuget push ([#7](https://www.github.com/miguelcouteirorodrigues/DockerTools/issues/7)) ([9eca828](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/9eca8282ea162427210b973ea0688f58d60d9640))
* Correct nuget push path ([#6](https://www.github.com/miguelcouteirorodrigues/DockerTools/issues/6)) ([dba17df](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/dba17df9dfa06ccd0d1fd4acdf0717d85ef74aa6))
* Correct path used to retrieve local package ([#5](https://www.github.com/miguelcouteirorodrigues/DockerTools/issues/5)) ([cbfe86f](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/cbfe86f6c5d723b6c3d2a86de11175c97fc1666b))
* Correct workflow build step ([#4](https://www.github.com/miguelcouteirorodrigues/DockerTools/issues/4)) ([f6b5e21](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/f6b5e21aa19529360438576088a38cca3c966c23))
* Remove unneeded references in csproj ([590189d](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/590189d9e432ad83f3edd12a234c06813e2b72e4))
* Revert Docker.Dotnet library and codes changes on container removal ([0907d0d](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/0907d0dbf1236e02febb3b8ebb8ac5943ecd1627))
* SqlServer healthcheck tweak ([20ef096](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/20ef096c609fd1b05c5a0cc4121d3ef2ad536761))
* Support xUnit SynchronizationContext ([ed9d494](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/ed9d49446f4ff4bf350421d1dddd6058d73f0127))
* Try to resolve nuget push path ([#8](https://www.github.com/miguelcouteirorodrigues/DockerTools/issues/8)) ([e524c2f](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/e524c2f15f6aee783ca46744d73544bfb11dfaff))
* Tweaks to container initialization ([9a5261b](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/9a5261bbd3a0d64fc909ff3de9f04079f97ecb78))

### Breaking Changes

* Alter how instances are initialized ([51d802f](https://www.github.com/miguelcouteirorodrigues/DockerTools/commit/51d802fc74958dcf73c0cdeab49c12981dfdff71))

