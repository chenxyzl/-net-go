package main

import (
	"time"

	"common"
)

func emp() {

}

func TestPluginInner(ii common.I1) int64 {
	var a = time.Now().UnixNano()
	var v = 0
	for i := 0; i < 100000000; i++ {
		emp()
		v += 1
	}
	var b = time.Now().UnixNano()
	return b - a
}

func TestPluginOuter(ii common.I1) int {
	return 1
}
