import {
  trigger,
  animate,
  transition,
  style,
  group
 } from '@angular/animations'

const animations =  [
  trigger('products', [
    transition('*=>void', [
      group([
        animate(1000, style({
          backgroundColor: 'red'
        })),
        animate(1000, style({
          transform: 'translateX(100px)',
          opacity: 0
        }))
      ])
    ])
  ])
]

export { animations }
