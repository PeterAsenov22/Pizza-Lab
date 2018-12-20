import {
  trigger,
  animate,
  transition,
  style,
  keyframes
 } from '@angular/animations'

const animations =  [
  trigger('productList', [
    transition('void=>*', [
      animate(1500, keyframes([
        style({
          opacity: 0,
          transform: 'translateX(-100px)'
        }),
        style({
          opacity: 0.5,
          transform: 'translateX(-50px)'
        }),
        style({
          opacity: 0.7,
          transform: 'translateX(-20px)'
        }),
        style({
          opacity: 1,
          transform: 'translateX(0)'
        })
      ]))
    ])
  ])
]

export { animations }
