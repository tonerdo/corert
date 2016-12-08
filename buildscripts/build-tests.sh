#!/usr/bin/env bash

scriptRoot="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

if [ "$BUILDVARS_DONE" != 1 ]; then
    . $scriptRoot/buildvars-setup.sh $*
fi

pushd ${__ProjectRoot}/tests
source ${__ProjectRoot}/tests/runtest.sh $__BuildOS $__BuildArch $__BuildType -dotnetclipath $__dotnetclipath
TESTERRORLEVEL=$?
popd